# My openfaas templates

I found official openfaas templates so minimal (and even cheap somehow), and they would not be covering my primary needs. Unfortunately, it is the best Faas platform around and we have no choice but stick with it (or ignore the concept of faas at all).

So I decided to create my own collection of openfaas templates, that covers the basics that is needed and enables me (and maybe others) use this platform.

## Usage
```
faas-cli template pull https://github.com/badihi/my-openfaas-templates
```

## Templates
For now, this repo contains templates for **C#** and **Node** based on **express**.

### C#
Forked from [CSharp http request](https://github.com/distantcam/csharp-httprequest-template), with following capabilities:

- Unit testing support with `xUnit` and `Moq`, so you don't have to deploy it every time for testing
- Providing multiple forms of response from function, including:
	- Text: ASCII result with content-type
	- File: Binary result with content-type
	- Redirect: Redirecting to a new url using `Location` header
- Access to the complete http request in the function

### Node.js Express

(TODO)
