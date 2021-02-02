/*void HTTP_init(void)
{
  HTTP.on("/RegistrationModel.json", RegistrationModel_JSON); // формирование configs.json страницы для передачи данных в web интерфейс
  HTTP.on("/DeviceStatusModel.json", DeviceStatusModel_JSON);
  HTTP.on("/VariableStatusModel.json", VariableStatusModel_JSON);
  HTTP.on("/DeviceVariablesConfig.json", DeviceVariablesConfig_JSON);
  HTTP.on("/VariableDataName.json", VariableDataName_JSON);
  // Запускаем HTTP сервер
  HTTP.begin();
}
*/
/*char RegistrationModel = "{
                         "GUID": "5473f20f - 05de - 4e75 - 91af - 686490030c38",
                         "name": "Arduino - test",
                         "brand": "arduino",
                         "model": "MEGA 2560",
                         "serial_number": "153255825568557862",
                         "last_changed": "2020 - 05 - 05T13: 04: 04Z",
                         "start_time": "2020 - 05 - 05T13: 04: 04Z",
                         "ip": "192.168.1.100",
                         "gateway": "192.168.1.100",
                         "mac": "24: D2: 78: 5B: 68: FF",
                         "timestamp": "2020 - 05 - 05T13: 04: 04Z",
                         "Internet_channel": "ETH"}";
*/
/*void RegistrationModel_JSON() {
  String RegistrationModel = "{";
  RegistrationModel += "\"GUID\":\"";
  RegistrationModel += "5473f20f - 05de - 4e75 - 91af - 686490030c38";
  RegistrationModel += "\",\"name\":\"";
  RegistrationModel += "Arduino - test";
  RegistrationModel += "\",\"brand\":\"";
  RegistrationModel += "arduino";
  RegistrationModel += "\",\"model\":\"";
  RegistrationModel += "MEGA 2560";
  RegistrationModel += "\",\"serial_number\":\"";
  RegistrationModel += "153255825568557862";
  RegistrationModel += "\",\"last_changed\":\"";
  RegistrationModel += "2020-05-05T13:04:04Z";
  RegistrationModel += "\",\"start_time\":\"";
  RegistrationModel += "2020-05-05T13:04:04Z";
  RegistrationModel += "\",\"ip\":\"";
  RegistrationModel += "192.168.1.100";
  RegistrationModel += "\",\"gateway\":\"";
  RegistrationModel += "192.168.1.100";
  RegistrationModel += "\",\"mac\":\"";
  RegistrationModel += "24:D2:78:5B:68:FF";
  RegistrationModel += "\",\"timestamp\":\"";
  RegistrationModel += "2020-05-05T13:04:04Z";
  RegistrationModel += "\",\"Internet_channel\":\"";
  RegistrationModel += "ETH";
  RegistrationModel += "\"}";
}
*/
/*char DeviceStatusModel = "{
                         "state": "enabled",
                         "heath": "ok",
                         "power": "battery",
                         "battery": 100.00,
                         "Internet_channel": "ETH"}";
*/
/*void DeviceStatusModel_JSON() {
  float _battary = 100.00;
  String DeviceStatusModel = "{";
  DeviceStatusModel += "\"state\":\"";
  DeviceStatusModel += "enabled";
  DeviceStatusModel += "\",\"heath\":\"";
  DeviceStatusModel += "ok";
  DeviceStatusModel += "\",\"power\":\"";
  DeviceStatusModel += "battery";
  DeviceStatusModel += "\",\"battery\":\"";
  DeviceStatusModel += _battary.toString();
  DeviceStatusModel += "\",\"Internet_channel\":\"";
  DeviceStatusModel += "ETH";
  DeviceStatusModel += "\"}";
}
*/
/*char VariableStatusModel = "{
                           "id": 1,
                           "name": "light",
                           "type": "IN",
                           "data_type": "STRING",
                           "enabled": true,
                           "timeout": 3000}";
*/
/*void VariableStatusModel_JSON() {
  int _id_var = 1;
  bool _enabled_var = true;
  int _timeout_var = 3000;
  String VariableStatusModel = "{";
  VariableStatusModel += "\"id\":\"";
  VariableStatusModel += _id_var;
  VariableStatusModel += "\",\"name\":\"";
  VariableStatusModel += "light";
  VariableStatusModel += "\",\"type\":\"";
  VariableStatusModel += "IN";
  VariableStatusModel += "\",\"data_type\":\"";
  VariableStatusModel += "STRING";
  VariableStatusModel += "\",\"enabled\":\"";
  VariableStatusModel += _enabled_var;
  VariableStatusModel += "\",\"timeout\":\"";
  VariableStatusModel += _timeout_var;
  VariableStatusModel += "\"}";
}
*/
/*char DeviceVariablesConfig = "{
                             "id": 1,
                             "enabled": true,
                             "timeout": 3000,
                             "section": true}";
*/
/*void DeviceVariablesConfig_JSON() {
  int _id_conf = 1;
  bool _enabled_conf = true;
  int _timeout_conf = 3000;
  bool _enabled_conf = true;
  String DeviceVariablesConfig  = "{";
  DeviceVariablesConfig += "\"id\":\"";
  DeviceVariablesConfig += _id_conf.toString();
  DeviceVariablesConfig += "\",\"enabled\":\"";
  DeviceVariablesConfig += _enabled_conf.toString();
  DeviceVariablesConfig += "\",\"timeout\":\"";
  DeviceVariablesConfig += _timeout_conf.toString();
  DeviceVariablesConfig += "\",\"section\":\"";
  DeviceVariablesConfig += _enabled_conf.toString();
  DeviceVariablesConfig += "\"}";
}
*/
/*char VariableDataName = "{
                        "id": 1,
                        "timestamp": "2020 - 05 - 05T13: 04: 04Z",
                        "value": "105.2"}";
*/
/*void VariableDataName_JSON() {
  int _id_data = 1;
  String VariableDataName = "{";
  VariableDataName += "\"id\":\"";
  VariableDataName += _id_data.toString();
  VariableDataName += "\",\"timestamp\":\"";
  VariableDataName += "2020-05-05T13:04:04Z";
  VariableDataName += "\",\"value\":\"";
  VariableDataName += "105.2";
  VariableDataName += "\"}";
  HTTP.send(200, "text/json", json);
}
*/
