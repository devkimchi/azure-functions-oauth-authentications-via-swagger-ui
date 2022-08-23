# Azure Functions OAuth Authentications via Swagger UI #

This provides sample app for authenticating Azure Functions endpoints by OAuth through the integrated Swagger UI.


## Prerequisites ##

To run this sample app on your local machine, you should be able to access [Azure Active Directory](https://docs.microsoft.com/azure/active-directory/fundamentals/active-directory-whatis?WT.mc_id=dotnet-44465-juyoo) in your tenant. Otherwise you can create a [free Azure Account](https://azure.microsoft.com/free/?WT.mc_id=dotnet-44465-juyoo).


## List of Authentications ##

* API Key in the querystring
* API Key in the request header
* Basic authentication
* Bearer authentication
* OAuth2 implicit flow
* OpenID Connect flow
* Easy Auth flow

The following OAuth2 auth flow doesn't support due to the limitations

* OAuth2 authorisation code flow - needs PKCE certificate that supports from OpenAPI spec v3.1.0
* OAuth2 client credentials flow - needs server/daemon


## Related Read ##

* 한국어: [OpenAPI 인증 플로우를 이용한 애저 펑션 엔드포인트 접근 권한 관리](https://blog.aliencube.org/ko/2021/10/06/securing-azure-function-endpoints-via-openapi-auth/)
* English: [Securing Azure Functions Endpoints via OpenAPI Auth](https://devkimchi.com/2021/10/06/securing-azure-function-endpoints-via-openapi-auth/)

