## requirements
Install vscode plugin: AWS Toolkit for Visual Studio Code
dotnet tool install -g Amazon.Lambda.Tools
bucket needs to be created before we run the dotnet lambda deploy-function command

## usefull commands
dotnet lambda deploy-function
        -After this command is run, you will be asked to input the runtime, supply "provided" as input.
        -Function name: dotnetcore-serverless-example
        -Memory: 256
        -Handler: not_required_for_custom_runtime

