<!-- ABOUT THE PROJECT -->
## Example C# Code For Posting to CyberSource APIs

This is sample code to show you how to post to CyberSource web APIs.  This sample is posting to the development environment at `https://apitest.cybersource.com`  From this code, you will be able to create the request headers necessary for your calls to be authorized by CyberSource.  There are some variations that need to be adjusted for calls to other methods and verbs, but the hardest part which is creating the acceptable headers is provided to you in this sample.  If you're still stuck, read in more details at https://sarn.phamornsuwana.com/c-code-for-cybersource-rest-apis/


<!-- GETTING STARTED -->
## Getting Started

You will need to have your merchant Id, secret key, and key Id ready.  Please put the [sample JSON file](rest-sample/json.txt) in your project's bin directory.

<!-- USAGE EXAMPLES -->
## Usage

The codebase is in .NET Core, since at this time (3/2019) the CyberSource Rest API SDK only works on the .NET Framework.  I'm assuming, most people who will find this sample code useful are those on .NET Core, issues compiling the SDK along with their other NUGETs, deployment workflow or just prefer to utilize the web APIs directly.  Please note that this code is ONLY for learning purposes and a proof of concept.  **It is NOT production ready code.**  Please refactor accordingly.


<!-- LICENSE -->
## License
Distributed under the MIT License. See `LICENSE` for more information.
