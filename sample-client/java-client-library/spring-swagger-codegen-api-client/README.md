# spring-swagger-settings-api-client

## Requirements

Building the API client library requires [Maven](https://maven.apache.org/) to be installed.

## Installation

To install the API client library to your local Maven repository, simply execute:

```shell
mvn install
```

To deploy it to a remote Maven repository instead, configure the settings of the repository and execute:

```shell
mvn deploy
```

Refer to the [official documentation](https://maven.apache.org/plugins/maven-deploy-plugin/usage.html) for more information.

### Maven users

Add this dependency to your project's POM:

```xml
<dependency>
    <groupId>com.settings</groupId>
    <artifactId>spring-swagger-settings-api-client</artifactId>
    <version>0.0.1-SNAPSHOT</version>
    <scope>compile</scope>
</dependency>
```

### Gradle users

Add this dependency to your project's build file:

```groovy
compile "com.settings:spring-swagger-settings-api-client:0.0.1-SNAPSHOT"
```

### Others

At first generate the JAR by executing:

    mvn package

Then manually install the following JARs:

* target/spring-swagger-settings-api-client-0.0.1-SNAPSHOT.jar
* target/lib/*.jar

## Getting Started

Please follow the [installation](#installation) instruction and execute the following Java code:

```java

import com.settings.client.invoker.*;
import com.settings.client.invoker.auth.*;
import come.settings.client.model.*;
import com.settings.client.api.ApplicationsApi;

import java.io.File;
import java.util.*;

public class ApplicationsApiExample {

    public static void main(String[] args) {
        
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
    }
}

```

## Documentation for API Endpoints

All URIs are relative to *https://localhost*

Class | Method | HTTP request | Description
------------ | ------------- | ------------- | -------------
*ApplicationsApi* | [**apiApplicationsAddParentparentAppIdNewnamePost**](docs/ApplicationsApi.md#apiApplicationsAddParentparentAppIdNewnamePost) | **POST** /api/applications/add/parent-{parentAppId}/new-{name} | 
*ApplicationsApi* | [**apiApplicationsByApplicationNameGet**](docs/ApplicationsApi.md#apiApplicationsByApplicationNameGet) | **GET** /api/applications/{applicationName} | 
*ApplicationsApi* | [**apiApplicationsGet**](docs/ApplicationsApi.md#apiApplicationsGet) | **GET** /api/applications | 
*AuthorizationApi* | [**apiAuthorizationJwtPost**](docs/AuthorizationApi.md#apiAuthorizationJwtPost) | **POST** /api/authorization/jwt | 
*AuthorizationApi* | [**apiAuthorizationLoginPost**](docs/AuthorizationApi.md#apiAuthorizationLoginPost) | **POST** /api/authorization/login | 
*EnvironmentsApi* | [**apiEnvironmentsAddParentparentEnvIdNewnamePost**](docs/EnvironmentsApi.md#apiEnvironmentsAddParentparentEnvIdNewnamePost) | **POST** /api/environments/add/parent-{parentEnvId}/new-{name} | 
*EnvironmentsApi* | [**apiEnvironmentsByEnvironmentNameGet**](docs/EnvironmentsApi.md#apiEnvironmentsByEnvironmentNameGet) | **GET** /api/environments/{environmentName} | 
*EnvironmentsApi* | [**apiEnvironmentsGet**](docs/EnvironmentsApi.md#apiEnvironmentsGet) | **GET** /api/environments | 
*SettingsApi* | [**apiSettingsByApplicationNameByEnvironmentNameGet**](docs/SettingsApi.md#apiSettingsByApplicationNameByEnvironmentNameGet) | **GET** /api/settings/{applicationName}/{environmentName} | 
*SettingsApi* | [**apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost**](docs/SettingsApi.md#apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost) | **POST** /api/settings/create-update/{applicationName}/{environmentName} | 


## Documentation for Models

 - [HierarchicalModel](docs/HierarchicalModel.md)
 - [JwtToken](docs/JwtToken.md)
 - [LoginModel](docs/LoginModel.md)
 - [SettingReadModel](docs/SettingReadModel.md)
 - [SettingWriteModel](docs/SettingWriteModel.md)
 - [SettingsWriteModel](docs/SettingsWriteModel.md)


## Documentation for Authorization

All endpoints do not require authorization.
Authentication schemes defined for the API:

## Recommendation

It's recommended to create an instance of `ApiClient` per thread in a multithreaded environment to avoid any potential issues.

## Author



