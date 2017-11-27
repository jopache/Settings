# EnvironmentsApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiEnvironmentsAddParentparentEnvIdNewnamePost**](EnvironmentsApi.md#apiEnvironmentsAddParentparentEnvIdNewnamePost) | **POST** /api/environments/add/parent-{parentEnvId}/new-{name} | 
[**apiEnvironmentsByEnvironmentNameGet**](EnvironmentsApi.md#apiEnvironmentsByEnvironmentNameGet) | **GET** /api/environments/{environmentName} | 
[**apiEnvironmentsGet**](EnvironmentsApi.md#apiEnvironmentsGet) | **GET** /api/environments | 


<a name="apiEnvironmentsAddParentparentEnvIdNewnamePost"></a>
# **apiEnvironmentsAddParentparentEnvIdNewnamePost**
> HierarchicalModel apiEnvironmentsAddParentparentEnvIdNewnamePost(name, parentEnvId)



### Example
```java
// Import classes:
//import com.settings.client.invoker.ApiException;
//import com.settings.client.api.EnvironmentsApi;


EnvironmentsApi apiInstance = new EnvironmentsApi();
String name = "name_example"; // String | 
Integer parentEnvId = 56; // Integer | 
try {
    HierarchicalModel result = apiInstance.apiEnvironmentsAddParentparentEnvIdNewnamePost(name, parentEnvId);
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling EnvironmentsApi#apiEnvironmentsAddParentparentEnvIdNewnamePost");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **name** | **String**|  |
 **parentEnvId** | **Integer**|  |

### Return type

[**HierarchicalModel**](HierarchicalModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

<a name="apiEnvironmentsByEnvironmentNameGet"></a>
# **apiEnvironmentsByEnvironmentNameGet**
> HierarchicalModel apiEnvironmentsByEnvironmentNameGet(environmentName)



### Example
```java
// Import classes:
//import com.settings.client.invoker.ApiException;
//import com.settings.client.api.EnvironmentsApi;


EnvironmentsApi apiInstance = new EnvironmentsApi();
String environmentName = "environmentName_example"; // String | 
try {
    HierarchicalModel result = apiInstance.apiEnvironmentsByEnvironmentNameGet(environmentName);
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling EnvironmentsApi#apiEnvironmentsByEnvironmentNameGet");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **environmentName** | **String**|  |

### Return type

[**HierarchicalModel**](HierarchicalModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

<a name="apiEnvironmentsGet"></a>
# **apiEnvironmentsGet**
> HierarchicalModel apiEnvironmentsGet()



### Example
```java
// Import classes:
//import com.settings.client.invoker.ApiException;
//import com.settings.client.api.EnvironmentsApi;


EnvironmentsApi apiInstance = new EnvironmentsApi();
try {
    HierarchicalModel result = apiInstance.apiEnvironmentsGet();
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling EnvironmentsApi#apiEnvironmentsGet");
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

