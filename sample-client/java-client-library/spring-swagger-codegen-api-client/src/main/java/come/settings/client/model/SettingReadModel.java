/*
 * My API
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */


package come.settings.client.model;

import java.util.Objects;
import com.fasterxml.jackson.annotation.JsonProperty;
import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonValue;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;

/**
 * SettingReadModel
 */
@javax.annotation.Generated(value = "io.swagger.codegen.languages.JavaClientCodegen", date = "2017-11-26T12:39:48.178-05:00")
public class SettingReadModel {
  @JsonProperty("name")
  private String name = null;

  @JsonProperty("value")
  private String value = null;

  @JsonProperty("applicationId")
  private Integer applicationId = null;

  @JsonProperty("environmentId")
  private Integer environmentId = null;

  @JsonProperty("applicationName")
  private String applicationName = null;

  @JsonProperty("environmentName")
  private String environmentName = null;

  @JsonProperty("applicationLeftWeight")
  private Integer applicationLeftWeight = null;

  @JsonProperty("environmentLeftWeight")
  private Integer environmentLeftWeight = null;

  public SettingReadModel name(String name) {
    this.name = name;
    return this;
  }

   /**
   * Get name
   * @return name
  **/
  @ApiModelProperty(value = "")
  public String getName() {
    return name;
  }

  public void setName(String name) {
    this.name = name;
  }

  public SettingReadModel value(String value) {
    this.value = value;
    return this;
  }

   /**
   * Get value
   * @return value
  **/
  @ApiModelProperty(value = "")
  public String getValue() {
    return value;
  }

  public void setValue(String value) {
    this.value = value;
  }

  public SettingReadModel applicationId(Integer applicationId) {
    this.applicationId = applicationId;
    return this;
  }

   /**
   * Get applicationId
   * @return applicationId
  **/
  @ApiModelProperty(value = "")
  public Integer getApplicationId() {
    return applicationId;
  }

  public void setApplicationId(Integer applicationId) {
    this.applicationId = applicationId;
  }

  public SettingReadModel environmentId(Integer environmentId) {
    this.environmentId = environmentId;
    return this;
  }

   /**
   * Get environmentId
   * @return environmentId
  **/
  @ApiModelProperty(value = "")
  public Integer getEnvironmentId() {
    return environmentId;
  }

  public void setEnvironmentId(Integer environmentId) {
    this.environmentId = environmentId;
  }

  public SettingReadModel applicationName(String applicationName) {
    this.applicationName = applicationName;
    return this;
  }

   /**
   * Get applicationName
   * @return applicationName
  **/
  @ApiModelProperty(value = "")
  public String getApplicationName() {
    return applicationName;
  }

  public void setApplicationName(String applicationName) {
    this.applicationName = applicationName;
  }

  public SettingReadModel environmentName(String environmentName) {
    this.environmentName = environmentName;
    return this;
  }

   /**
   * Get environmentName
   * @return environmentName
  **/
  @ApiModelProperty(value = "")
  public String getEnvironmentName() {
    return environmentName;
  }

  public void setEnvironmentName(String environmentName) {
    this.environmentName = environmentName;
  }

  public SettingReadModel applicationLeftWeight(Integer applicationLeftWeight) {
    this.applicationLeftWeight = applicationLeftWeight;
    return this;
  }

   /**
   * Get applicationLeftWeight
   * @return applicationLeftWeight
  **/
  @ApiModelProperty(value = "")
  public Integer getApplicationLeftWeight() {
    return applicationLeftWeight;
  }

  public void setApplicationLeftWeight(Integer applicationLeftWeight) {
    this.applicationLeftWeight = applicationLeftWeight;
  }

  public SettingReadModel environmentLeftWeight(Integer environmentLeftWeight) {
    this.environmentLeftWeight = environmentLeftWeight;
    return this;
  }

   /**
   * Get environmentLeftWeight
   * @return environmentLeftWeight
  **/
  @ApiModelProperty(value = "")
  public Integer getEnvironmentLeftWeight() {
    return environmentLeftWeight;
  }

  public void setEnvironmentLeftWeight(Integer environmentLeftWeight) {
    this.environmentLeftWeight = environmentLeftWeight;
  }


  @Override
  public boolean equals(java.lang.Object o) {
    if (this == o) {
      return true;
    }
    if (o == null || getClass() != o.getClass()) {
      return false;
    }
    SettingReadModel settingReadModel = (SettingReadModel) o;
    return Objects.equals(this.name, settingReadModel.name) &&
        Objects.equals(this.value, settingReadModel.value) &&
        Objects.equals(this.applicationId, settingReadModel.applicationId) &&
        Objects.equals(this.environmentId, settingReadModel.environmentId) &&
        Objects.equals(this.applicationName, settingReadModel.applicationName) &&
        Objects.equals(this.environmentName, settingReadModel.environmentName) &&
        Objects.equals(this.applicationLeftWeight, settingReadModel.applicationLeftWeight) &&
        Objects.equals(this.environmentLeftWeight, settingReadModel.environmentLeftWeight);
  }

  @Override
  public int hashCode() {
    return Objects.hash(name, value, applicationId, environmentId, applicationName, environmentName, applicationLeftWeight, environmentLeftWeight);
  }


  @Override
  public String toString() {
    StringBuilder sb = new StringBuilder();
    sb.append("class SettingReadModel {\n");
    
    sb.append("    name: ").append(toIndentedString(name)).append("\n");
    sb.append("    value: ").append(toIndentedString(value)).append("\n");
    sb.append("    applicationId: ").append(toIndentedString(applicationId)).append("\n");
    sb.append("    environmentId: ").append(toIndentedString(environmentId)).append("\n");
    sb.append("    applicationName: ").append(toIndentedString(applicationName)).append("\n");
    sb.append("    environmentName: ").append(toIndentedString(environmentName)).append("\n");
    sb.append("    applicationLeftWeight: ").append(toIndentedString(applicationLeftWeight)).append("\n");
    sb.append("    environmentLeftWeight: ").append(toIndentedString(environmentLeftWeight)).append("\n");
    sb.append("}");
    return sb.toString();
  }

  /**
   * Convert the given object to string with each line indented by 4 spaces
   * (except the first line).
   */
  private String toIndentedString(java.lang.Object o) {
    if (o == null) {
      return "null";
    }
    return o.toString().replace("\n", "\n    ");
  }
  
}

