package com.settings.settingsjavaclientapp;

import com.settings.client.api.SettingsApi;
import come.settings.client.model.SettingReadModel;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.bind.annotation.RequestMapping;

import java.util.Iterator;
import java.util.List;

@RestController
public class TestController {
    @Autowired
    private SettingsApi settingsApi;

    @RequestMapping("/")
    public String index() {
        List<SettingReadModel> settingReadModels = settingsApi.apiSettingsByApplicationNameByEnvironmentNameGet("Engineering", "All");
        String theValues = "";
        for(Iterator<SettingReadModel> i = settingReadModels.iterator(); i.hasNext();) {
            SettingReadModel model = i.next();
            theValues += model.getName() + " : " + model.getValue() + "<br />";
        }
        return "I'm in ur Java Spring web application serving up Settings service backed configuration values: <br /><br /> Setting Name : Value <br />" + theValues;
    }
}