using Agents.Common.Interfaces;
using Agents.Common.Storage;

namespace VotingBasedCooperation;

public class UserStories
{
    public static ContextData U001 =>
        ContextData.MergeAll(("US001",
            """
            # [US001] Create endpoint for client registration

            ### User Narrative
            As a CRM system operator,  
            I want to register a new client through the API,  
            so that the data can be persisted in the database and made available for future consultation.

            ### Objective
            Implement a `POST /api/clients` endpoint in the application's REST API, allowing the registration of new clients with required field validations.

            ### Assumptions
            - The system already has a persistence infrastructure (Entity Framework Core).
            - There is a `Clients` table in the database.
            - The domain model `Client` is already partially defined.
            - The system uses ASP.NET Core 7.0.
            - API authentication is already configured using JWT.

            ### Acceptance Criteria
            - The endpoint must be available at `POST /api/clients`.
            - The following fields must be required: `Name`, `Email`, `BirthDate`.
            - The `Email` field must be unique and validated.
            - The client must be successfully persisted in the database.
            - The endpoint must return HTTP 201 Created with the ID of the newly created client.
            - In case of validation error, it must return HTTP 400 with descriptive messages.
            - The endpoint must be protected with JWT authentication.

            ### Test Scenarios
            ```gherkin
            Scenario: Successful client registration
              Given I am authenticated with a valid JWT token
              And I send a POST request to /api/clients with valid data
              When the server processes the request
              Then the client is saved in the database
              And the server responds with HTTP 201 Created and the client's ID

            Scenario: Attempt to register with invalid email
              Given I am authenticated with a valid JWT token
              And I send a POST request to /api/clients with an invalid email
              When the server processes the request
              Then the server responds with HTTP 400 Bad Request
              And a message indicating that the email is invalid

            Scenario: Attempt to register without authentication
              Given I do not send a JWT token
              When I send a POST request to /api/clients
              Then the server responds with HTTP 401 Unauthorized
            ```
            ### Technical Information
            - ASP.NET Core 8.0
            - Controller: ClientsController
            - Endpoint: [HttpPost("/api/clients")]
            - Input model: ClientDto
            - Entity: Client
            - Validation using DataAnnotations
            - Persistence with Entity Framework Core
            - Authentication middleware already enabled
            - Returns response using CreatedAtAction 
            """));
}