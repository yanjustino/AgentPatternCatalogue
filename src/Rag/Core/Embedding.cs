using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Rag.Core.Interfaces;

namespace Rag.Core;

/// <summary>
/// Represents an embedding class responsible for generating vector embeddings
/// for a given text input using a specified ONNX model and tokenizer.
/// </summary>
public class Embedding(string modelPath, ITokenizer tokenizer) : IEmbedding
{
    private readonly InferenceSession _session = new(modelPath);

    /// <summary>
    /// Generates a vector embedding for the given text input using the specified tokenizer
    /// and ONNX model.
    /// </summary>
    /// <param name="text">The input text to generate the embedding for.</param>
    /// <returns>
    /// A float array representing the generated vector embedding for the given input text.
    /// </returns>
    public float[] Generate(string text)
    {
        var inputIds = tokenizer.Encode(text);
        var length = inputIds.Length;

        var inputTensor = new DenseTensor<long>(new[] { 1, length });
        var attentionMask = new DenseTensor<long>(new[] { 1, length });

        for (var i = 0; i < length; i++)
        {
            inputTensor[0, i] = inputIds[i];
            attentionMask[0, i] = 1; // tudo ativo, sem padding
        }

        var inputs = new List<NamedOnnxValue>
        {
            NamedOnnxValue.CreateFromTensor("input_ids", inputTensor),
            NamedOnnxValue.CreateFromTensor("attention_mask", attentionMask)
        };

        using var results = _session.Run(inputs);
        var output = results[0].AsTensor<float>();

        return MeanPooling(output);
    }

    /// <summary>
    /// Performs mean pooling on the given tensor to produce the final vector
    /// representation by averaging along the sequence length dimension.
    /// </summary>
    /// <param name="tensor">The input tensor containing embeddings with dimensions
    /// [batch_size, sequence_length, embedding_dimension].</param>
    /// <returns>
    /// A float array representing the averaged embedding vector for the input tensor.
    /// </returns>
    private static float[] MeanPooling(Tensor<float> tensor)
    {
        var batch = tensor.Dimensions[0]; // 1
        var seqLen = tensor.Dimensions[1];
        var dim = tensor.Dimensions[2];

        var embedding = new float[dim];

        for (var i = 0; i < seqLen; i++)
        {
            for (var j = 0; j < dim; j++)
            {
                embedding[j] += tensor[0, i, j];
            }
        }

        for (var j = 0; j < dim; j++)
        {
            embedding[j] /= seqLen;
        }

        return embedding;
    }
}