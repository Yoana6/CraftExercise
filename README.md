## Table of Contents
- [Prerequisites](#prerequisites)
- [Setup](#setup)
- [Environment Variables](#environment-variables)
- [Usage](#usage)
- [Running Tests](#running-tests)
- [Troubleshooting](#troubleshooting)

## Prerequisites
- .NET Core SDK 3.1 or later
- A GitHub account with a Personal Access Token (PAT)
- A Freshdesk account with an API Key

## Setup

1. **Clone the repository:**
    ```bash
    git clone https://github.com/your-username/github-freshdesk-integration.git
    cd github-freshdesk-integration
    ```

2. **Set up environment variables:**

    For Windows:
    ```bash
    setx GITHUB_TOKEN "your_github_token"
    setx FRESHDESK_API_KEY "your_freshdesk_api_key"
    setx FRESHDESK_SUBDOMAIN "your_freshdesk_subdomain"
    ```
3. **Restore dependencies:**
    ```bash
    dotnet restore
    ```

4. **Build the project:**
    ```bash
    dotnet build
    ```

## Environment Variables

The application relies on the following environment variables:

- `GITHUB_TOKEN`: Your GitHub Personal Access Token.
- `FRESHDESK_API_KEY`: Your Freshdesk API Key.
- `FRESHDESK_SUBDOMAIN`: Your Freshdesk subdomain (e.g., if your Freshdesk URL is `https://yourcompany.freshdesk.com`, the subdomain is `yourcompany`).

## Usage

1. **Run the application:**
    ```bash
    dotnet run
    ```

2. Open a browser and navigate to `https://localhost:7072` to access the application.

## Running Tests

1. **Navigate to the test project directory:**
    ```bash
    cd tests
    ```

2. **Run the tests:**
    ```bash
    dotnet test
    ```

## Troubleshooting

### HTTP 404 Error
If you encounter an HTTP 404 error when accessing `https://localhost:7072`, ensure that:
- The application is running.
- You have set the environment variables correctly.
- You are using the correct URL.

### Environment Variable Issues
If the environment variables are not being recognized:
- Ensure that you have restarted your terminal or command prompt after setting the environment variables.
- Verify that the environment variables are correctly set by running `echo $GITHUB_TOKEN` (macOS/Linux) or `echo %GITHUB_TOKEN%` (Windows).

### GitHub API Errors
If you encounter errors related to the GitHub API:
- Verify that your GitHub token has the necessary permissions (e.g., `read:user`).

### Freshdesk API Errors
If you encounter errors related to the Freshdesk API:
- Ensure that your Freshdesk API key and subdomain are correct.
- Verify that the API key has the necessary permissions.

If you continue to experience issues, please refer to the official documentation for GitHub and Freshdesk APIs, or seek help from the community.

## Contributing

If you would like to contribute to this project, please fork the repository and submit a pull request. Contributions are welcome!

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
