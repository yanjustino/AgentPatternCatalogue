# Configuring development Environment

## Dependencies

- .Net 9 >=
- Python 3 >=
- Ollama 0.6 >=
- phi4-mini

## Download vocabulary
Download the vocab.txt file in https://huggingface.co/intfloat/e5-small-v2/blob/main/vocab.txt

## Baixar e usar arquivo model.onnx
Download file in  https://huggingface.co/intfloat/e5-small-v2/

To download and adapt use the follow script

```python
python -m optimum.exporters.onnx \
  --model intfloat/e5-small-v2 \
  --task feature-extraction \
  --opset 14 \ 
  --library-name sentence_transformers \
  ./e5-small-v2-onnx
```

