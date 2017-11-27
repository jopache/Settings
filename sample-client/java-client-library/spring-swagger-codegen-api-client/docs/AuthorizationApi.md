# AuthorizationApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiAuthorizationJwtPost**](AuthorizationApi.md#apiAuthorizationJwtPost) | **POST** /api/authorization/jwt | 
[**apiAuthorizationLoginPost**](AuthorizationApi.md#apiAuthorizationLoginPost) | **POST** /api/authorization/login | 


<a name="apiAuthorizationJwtPost"></a>
# **apiAuthorizationJwtPost**
> JwtToken apiAuthorizationJwtPost(model)



### Example
```java
// Import classes:
//import com.settings.client.invoker.ApiException;
//import com.settings.client.api.AuthorizationApi;


AuthorizationApi apiInstance = new AuthorizationApi();
LoginModel model = new LoginModel(); // LoginModel | 
try {
    JwtToken result = apiInstance.apiAuthorizationJwtPost(model);
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling AuthorizationApi#apiAuthorizationJwtPost");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **model** | [**LoginModel**](LoginModel.md)|  | [optional]

### Return type

[**JwtToken**](JwtToken.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

<a name="apiAuthorizationLoginPost"></a>
# **apiAuthorizationLoginPost**
> apiAuthorizationLoginPost(model)



### Example
```java
// Import classes:
//import com.settings.client.invoker.ApiException;
//import com.settings.client.api.AuthorizationApi;


AuthorizationApi apiInstance = new AuthorizationApi();
LoginModel model = new LoginModel(); // LoginModel | 
try {
    apiInstance.apiAuthorizationLoginPost(model);
} catch (ApiException e) {
    System.err.println("Exception when calling AuthorizationApi#apiAuthorizationLoginPost");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **model** | [**LoginModel**](LoginModel.md)|  | [optional]

### Return type

null (empty response body)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: Not defined

