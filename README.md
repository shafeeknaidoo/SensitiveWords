# SensitiveWords API

Rudimentary API that masks offending words with an asterisk "*"
The list of words are contained within a sqllite database.

# Usage
POST /api/SensitiveWords
body: "Text to be parsed"

# Deployment
## Using Azure Devops
- Create a deployment group in azure devops
- setup host computer, ensuring IIS is installed and configured. Ensure the deployment group name aligns with teh deployment group on Azure
- Create a new pipeline to build the application ona configured agent
- create a deployment pipeline using the deployment group defined previously
- Navigate to host server and verify web app is successfully installed
