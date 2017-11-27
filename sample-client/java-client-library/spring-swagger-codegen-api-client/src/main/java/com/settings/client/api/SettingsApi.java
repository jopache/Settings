package com.settings.client.api;

import com.settings.client.invoker.ApiClient;

import come.settings.client.model.SettingReadModel;
import come.settings.client.model.SettingsWriteModel;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import org.springframework.util.LinkedMultiValueMap;
import org.springframework.util.MultiValueMap;
import org.springframework.web.client.RestClientException;
import org.springframework.web.client.HttpClientErrorException;
import org.springframework.web.util.UriComponentsBuilder;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.core.io.FileSystemResource;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;

@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaClientCodegen", date = "2017-11-26T12:39:48.178-05:00")
@Component("com.settings.client.api.SettingsApi")
public class SettingsApi {
    private ApiClient apiClient;

    public SettingsApi() {
        this(new ApiClient());
    }

    @Autowired
    public SettingsApi(ApiClient apiClient) {
        this.apiClient = apiClient;
    }

    public ApiClient getApiClient() {
        return apiClient;
    }

    public void setApiClient(ApiClient apiClient) {
        this.apiClient = apiClient;
    }

    /**
     * 
     * 
     * <p><b>200</b> - Success
     * @param applicationName The applicationName parameter
     * @param environmentName The environmentName parameter
     * @return List&lt;SettingReadModel&gt;
     * @throws RestClientException if an error occurs while attempting to invoke the API
     */
    public List<SettingReadModel> apiSettingsByApplicationNameByEnvironmentNameGet(String applicationName, String environmentName) throws RestClientException {
        Object postBody = null;
        
        // verify the required parameter 'applicationName' is set
        if (applicationName == null) {
            throw new HttpClientErrorException(HttpStatus.BAD_REQUEST, "Missing the required parameter 'applicationName' when calling apiSettingsByApplicationNameByEnvironmentNameGet");
        }
        
        // verify the required parameter 'environmentName' is set
        if (environmentName == null) {
            throw new HttpClientErrorException(HttpStatus.BAD_REQUEST, "Missing the required parameter 'environmentName' when calling apiSettingsByApplicationNameByEnvironmentNameGet");
        }
        
        // create path and map variables
        final Map<String, Object> uriVariables = new HashMap<String, Object>();
        uriVariables.put("applicationName", applicationName);
        uriVariables.put("environmentName", environmentName);
        String path = UriComponentsBuilder.fromPath("/api/settings/{applicationName}/{environmentName}").buildAndExpand(uriVariables).toUriString();
        
        final MultiValueMap<String, String> queryParams = new LinkedMultiValueMap<String, String>();
        final HttpHeaders headerParams = new HttpHeaders();
        final MultiValueMap<String, Object> formParams = new LinkedMultiValueMap<String, Object>();

        final String[] accepts = { 
            "text/plain", "application/json", "text/json"
        };
        final List<MediaType> accept = apiClient.selectHeaderAccept(accepts);
        final String[] contentTypes = { };
        final MediaType contentType = apiClient.selectHeaderContentType(contentTypes);

        String[] authNames = new String[] {  };

        ParameterizedTypeReference<List<SettingReadModel>> returnType = new ParameterizedTypeReference<List<SettingReadModel>>() {};
        return apiClient.invokeAPI(path, HttpMethod.GET, queryParams, postBody, headerParams, formParams, accept, contentType, authNames, returnType);
    }
    /**
     * 
     * 
     * <p><b>200</b> - Success
     * @param applicationName The applicationName parameter
     * @param environmentName The environmentName parameter
     * @param settings The settings parameter
     * @return Boolean
     * @throws RestClientException if an error occurs while attempting to invoke the API
     */
    public Boolean apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost(String applicationName, String environmentName, SettingsWriteModel settings) throws RestClientException {
        Object postBody = settings;
        
        // verify the required parameter 'applicationName' is set
        if (applicationName == null) {
            throw new HttpClientErrorException(HttpStatus.BAD_REQUEST, "Missing the required parameter 'applicationName' when calling apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost");
        }
        
        // verify the required parameter 'environmentName' is set
        if (environmentName == null) {
            throw new HttpClientErrorException(HttpStatus.BAD_REQUEST, "Missing the required parameter 'environmentName' when calling apiSettingsCreateUpdateByApplicationNameByEnvironmentNamePost");
        }
        
        // create path and map variables
        final Map<String, Object> uriVariables = new HashMap<String, Object>();
        uriVariables.put("applicationName", applicationName);
        uriVariables.put("environmentName", environmentName);
        String path = UriComponentsBuilder.fromPath("/api/settings/create-update/{applicationName}/{environmentName}").buildAndExpand(uriVariables).toUriString();
        
        final MultiValueMap<String, String> queryParams = new LinkedMultiValueMap<String, String>();
        final HttpHeaders headerParams = new HttpHeaders();
        final MultiValueMap<String, Object> formParams = new LinkedMultiValueMap<String, Object>();

        final String[] accepts = { 
            "text/plain", "application/json", "text/json"
        };
        final List<MediaType> accept = apiClient.selectHeaderAccept(accepts);
        final String[] contentTypes = { 
            "application/json-patch+json", "application/json", "text/json", "application/_*+json"
        };
        final MediaType contentType = apiClient.selectHeaderContentType(contentTypes);

        String[] authNames = new String[] {  };

        ParameterizedTypeReference<Boolean> returnType = new ParameterizedTypeReference<Boolean>() {};
        return apiClient.invokeAPI(path, HttpMethod.POST, queryParams, postBody, headerParams, formParams, accept, contentType, authNames, returnType);
    }
}
