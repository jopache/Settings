package com.settings.client.api;

import com.settings.client.invoker.ApiClient;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class SettingsIntegrationConfig {

    @Bean
    public SettingsApi petApi() {
        return new SettingsApi(apiClient());
    }

    @Bean
    public ApiClient apiClient() {
        return new ApiClient();
    }
}