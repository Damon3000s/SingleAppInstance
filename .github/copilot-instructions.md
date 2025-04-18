# Copilot Instructions

## Using Your Memory (Knowledge Graph)

### Memory Tools Reference

Your memory is managed by the `mcp-knowledge-graph` server, which provides these tools:

**Reading Tools:**

- `read_graph` - Access the entire memory graph for a comprehensive project overview
- `search_nodes` - Query for memories related to specific search terms or keywords
- `open_nodes` - Access memories about specific entities and their relationships

**Writing Tools:**

- `create_entities` - Create new entities in your memory
- `create_relations` - Create relationships between entities
- `add_observations` - Add observations to existing entities

**Maintenance Tools:**

- `delete_entities` - Remove entities from your memory
- `delete_relations` - Remove relationships between entities
- `delete_observations` - Remove specific observations from entities

> Note: Tool names may be prefixed with characters (e.g., "9f1_"). Match the name with available tools and ask for clarification if needed.

### Using Your Memory Effectively

- **Start each chat** by recalling project information with the `read_graph` tool
- **Respond to all user messages** by memorizing details from their message, and the whole conversation so far using the memory writing tools
- If you need **specific information**, use `search_nodes` or `open_nodes`
- If information is **missing**, ask for additional context about specific entities or relationships
- When reviewing memory, offer to:
  - Correct outdated or incorrect information
  - Fill in incomplete or missing information
  - Remove irrelevant or unnecessary information
  - Resolve inconsistencies or contradictions
  - Clarify ambiguous or unclear information
  - Consolidate redundant or duplicated information

### Project Memory Organization

- Organize your memory using appropriate entity types:
  - "Command" - CLI commands, operations, or actions
  - "Class" - Code classes and their relationships
  - "Function" - Important functions or methods
  - "Pattern" - Design patterns in the codebase
  - "Code Structure" - Architectural elements
  - "TechnologyStack" - Technologies, frameworks, libraries
  - "Project" - Overall project information
  - "Concept" - High-level domain concepts
  - "User" - User roles, personas, or profiles
  - "Developer" - Developer roles, personas, or profiles
  - "Task" - Tasks a user or developer could perform, or has performed
  - "Challenge" - Challenges a user or developer could face, or has faced
  - "Goal" - Goals a user or developer could achieve, or has achieved
  - "Requirement" - Requirements a user or developer could have, or has had
  - "User Story" - User stories that describe features from an end-user perspective
  - "Feature" - Features of the project or product

- When memorizing **commands or operations**, include:
  - Purpose and functionality
  - Required parameters and options
  - Authentication or permission requirements
  - Dependencies or prerequisites
  - Specific behaviors or edge cases

- When memorizing **code components**, prioritize:
  - Class hierarchy relationships and inheritance patterns
  - Functional relationships between components
  - Caching or optimization mechanisms
  - Error handling patterns
  - Parameter validation logic and error states

- When memorizing **project structure**, focus on:
  - Directory structure and organization
  - File naming conventions and patterns
  - Dependency management and versioning
  - Build and deployment processes
  - Testing and quality assurance practices
  - Continuous integration and deployment practices
  - Documentation practices and standards

- When memorizing **user or developer roles and tasks**, consider:
  - Personas and profiles
  - Stories and use cases
  - Goals and challenges
  - Tasks and workflows
  - Feedback and suggestions

### Managing Information You Don't Know

If you lack relevant information in your memory:

1. Begin with "Analyzing project..."
2. Enumerate code and documentation files in the repository
3. Analyze project structure and dependencies
4. Commit all discoveries using `create_entities`, `create_relations`, and `add_observations` tools

Focus on capturing:

- Repository purpose and technologies
- User tasks, challenges, goals
- Coding standards, requirements, constraints
- Best practices and improvement areas
- Project structure, dependencies, features
- Libraries, frameworks, performance and security considerations

During all chats, continually update your memory with new information, establishing proper connections between entities and adding relevant observations. Check for consistency and correctness in your stored knowledge.

### General Project Knowledge Management

- Remember key relationships:
  - Architectural patterns (MVC, MVVM, microservices, etc.)
  - Component hierarchies and dependencies
  - API interactions and data flows
  - Caching or optimization strategies
  - Security and authentication mechanisms

- Track important evolving aspects:
  - Core component and functionality changes
  - External API integration updates
  - Authentication/security modifications
  - Optimization strategy changes
  - New features or components
  - Error handling pattern changes
  - Validation logic updates
  - Documentation improvements

## Workflow and Process Guidelines

- Before running any command, check if you have a specialized tool for the task and use the tool if available
- If you make code changes, ensure you immediately run the appropriate commands to verify the changes for style conformance, and build/test success
- After any operation, check if you need to update your memory with new information or observations
- If you encounter any issues or errors, check the documentation for the specific command or tool you are using

## Coding Guidelines and Style Guides

### General Coding Guidelines

- Follow the coding guidelines and style guides defined in the project
- Follow best practices for code organization and structure
- Ensure code is modular and reusable where possible
- Keep functions and methods focused on a single responsibility
- Use meaningful variable and method names that convey their purpose
- Avoid using magic numbers or hard-coded values; use constants or configuration settings instead
- Use consistent naming conventions for variables, methods, and classes
- Use appropriate error handling and logging mechanisms
- Avoid using global variables or state where possible
- Use the appropriate version of the programming language as specified in the project
- Follow official language-specific guidelines and best practices
- Ensure all dependencies are compatible with the specified version
- Follow the official language coding conventions and guidelines
- Use any project-wide settings defined in configuration files
- Use appropriate formatting tools to ensure code adheres to project style guidelines
- Use appropriate build commands to verify code compiles correctly
- Use appropriate testing commands to ensure code functions as expected
- Lines in code files should not contain trailing whitespace, especially blank lines

### Documentation Guidelines

- Follow any linting rules for documentation files
- Use appropriate formatting for documentation based on the file format (.md, .rst, etc.)
- Keep documentation concise, accurate, and up-to-date
- Include examples where appropriate

### Comment Guidelines

- Avoid using comments to explain simple or obvious code. The code should be self-explanatory, and comments should provide additional context or reasoning
- Avoid using comments to explain the purpose of a class or method. The class or method name should be descriptive enough to convey its purpose
- Write clear and concise comments to explain complex logic, important decisions or trade-offs, and any non-obvious behavior in the code
- Use comments to document any assumptions, limitations, or potential issues with the code
- Use TODO comments to indicate areas of the code that require further work or attention. They should be clear and specific, indicating what needs to be done and why
- Don't use journal-style comments (e.g., "Fix for issue #123") in the codebase, use version control to provide a history of changes and issues

### Language-Specific Guidelines

#### .NET/C# Guidelines

- Use the latest stable version of .NET and C# as specified in the project
- Follow the official C# coding conventions and guidelines
- Use the .NET CLI for building, testing, and running the project, you can use the `-v d` option to get detailed output when required
- Before trying to fix any build errors, use the .NET format tool to format the code according to the project's style guidelines and check again to see if the errors persist
- Use `mstest` when writing unit tests
- Use `dotnet test --collect:"XPlat Code Coverage"` to collect code coverage data for the tests
- Check if `Directory.Build.targets` has the required references before adding new references to the project file
- Review test results and coverage reports to identify areas for improvement

#### Markdown Guidelines

- Use `markdownlint --fix <path-to-markdown-files>` to automatically fix any linting issues in markdown files
