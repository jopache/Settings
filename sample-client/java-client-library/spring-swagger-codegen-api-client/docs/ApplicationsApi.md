# ApplicationsApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiApplicationsAddParentparentAppIdNewnamePost**](ApplicationsApi.md#apiApplicationsAddParentparentAppIdNewnamePost) | **POST** /api/applications/add/parent-{parentAppId}/new-{name} | 
[**apiApplicationsByApplicationNameGet**](ApplicationsApi.md#apiApplicationsByApplicationNameGet) | **GET** /api/applications/{applicationName} | 
[**apiApplicationsGet**](ApplicationsApi.md#apiApplicationsGet) | **GET** /api/applications | 


<a name="apiApplicationsAddParentparentAppIdNewnamePost"></a>
# **apiApplicationsAddParentparentAppIdNewnamePost**
> HierarchicalModel apiApplicationsAddParentparentAppIdNewnamePost(name, parentAppId)



### Example
```java
// Import classes:
//import com.settings.client.invoker.ApiException;
//import com.settings.client.api.ApplicationsApi;


ApplicationsApi apiInstance = new ApplicationsApi();
String name = "name_example"; // String | 
Integer parentAppId = 56; // Integer | 
try {
    HierarchicalModel result = apiInstance.apiApplicationsAddParentparentAppIdNewnamePost(name, parentAppId);
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling ApplicationsApi#apiApplicationsAddParentparentAppIdNewnamePost");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **name** | **String**|  |
 **parentAppId** | **Integer**|  |

### Return type

[**HierarchicalModel**](HierarchicalModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

<a name="apiApplicationsByApplicationNameGet"></a>
# **apiApplicationsByApplicationNameGet**
> HierarchicalModel apiApplicationsByApplicationNameGet(applicationName)



### Example
```java
// Import classes:
//import com.settings.client.invoker.ApiException;
//import com.settings.client.api.ApplicationsApi;


ApplicationsApi apiInstance = new ApplicationsApi();
String applicationName = "applicationName_example"; // String | 
try {
    HierarchicalModel result = apiInstance.apiApplicationsByApplicationNameGet(applicationName);
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling ApplicationsApi#apiApplicationsByApplicationNameGet");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **applicationName** | **String**|  |

### Return type

[**HierarchicalModel**](HierarchicalModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

<a name="apiApplicationsGet"></a>
# **apiApplicationsGet**
> HierarchicalModel apiApplicationsGet()



### Example
```java
// Import classes:
//import com.settings.client.invoker.ApiException;
//import com.settings.client.api.ApplicationsApi;


ApplicationsApi apiInstance = new ApplicationsApi();
try {
    HierarchicalModel result = apiInstance.apiApplicationsGet();
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling ApplicationsApi#apiApplicationsGet");
    e.printStackTrace();
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**HierarchicalModel**](HierarchicalModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

