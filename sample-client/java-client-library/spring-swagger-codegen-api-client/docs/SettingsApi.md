# SettingsApi

All URIs are relative to *https://localhost*

Method | HTTP request | Description
------------- | ------------- | -------------
[**apiSettingsByApplicationNameByEnvironmentNameGet**](SettingsApi.md#apiSettingsByApplicationNameByEnvironmentNameGet) | **GET** /api/settings/{applicationName}/{environmentName} | 
[**apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost**](SettingsApi.md#apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost) | **POST** /api/settings/create-update/{applicationName}/{environmentName} | 


<a name="apiSettingsByApplicationNameByEnvironmentNameGet"></a>
# **apiSettingsByApplicationNameByEnvironmentNameGet**
> List&lt;SettingReadModel&gt; apiSettingsByApplicationNameByEnvironmentNameGet(applicationName, environmentName)



### Example
```java
// Import classes:
//import com.settings.client.invoker.ApiException;
//import com.settings.client.api.SettingsApi;


SettingsApi apiInstance = new SettingsApi();
String applicationName = "applicationName_example"; // String | 
String environmentName = "environmentName_example"; // String | 
try {
    List<SettingReadModel> result = apiInstance.apiSettingsByApplicationNameByEnvironmentNameGet(applicationName, environmentName);
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling SettingsApi#apiSettingsByApplicationNameByEnvironmentNameGet");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **applicationName** | **String**|  |
 **environmentName** | **String**|  |

### Return type

[**List&lt;SettingReadModel&gt;**](SettingReadModel.md)

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: text/plain, application/json, text/json

<a name="apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost"></a>
# **apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost**
> Boolean apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost(applicationName, environmentName, settings)



### Example
```java
// Import classes:
//import com.settings.client.invoker.ApiException;
//import com.settings.client.api.SettingsApi;


SettingsApi apiInstance = new SettingsApi();
String applicationName = "applicationName_example"; // String | 
String environmentName = "environmentName_example"; // String | 
SettingsWriteModel settings = new SettingsWriteModel(); // SettingsWriteModel | 
try {
    Boolean result = apiInstance.apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost(applicationName, environmentName, settings);
    System.out.println(result);
} catch (ApiException e) {
    System.err.println("Exception when calling SettingsApi#apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost");
    e.printStackTrace();
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **applicationName** | **String**|  |
 **environmentName** | **String**|  |
 **settings** | [**SettingsWriteModel**](SettingsWriteModel.md)|  | [optional]

### Return type

**Boolean**

### Authorization

No authorization required

### HTTP request headers

 - **Content-Type**: application/json-patch+json, application/json, text/json, application/_*+json
 - **Accept**: text/plain, application/json, text/json

