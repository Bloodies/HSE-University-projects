

/*
  Name:    Online.ino
  Created: 13.03.2020 0:38:05
  Author:  Bloodies
*/

extern "C"
{
#include "user_interface.h";
}
#include <Ticker.h>
#include <Ethernet.h>
#include <AsyncMqttClient.h>

String Topic_95031111_1;
byte QoS_95031111_1;
bool Received_95031111_1;
String Message_95031111_1;
bool subscribed_95031111_1;
bool Send_200064703_1;
String Topic_200064703_1;
String Message_200064703_1;
byte QoS_200064703_1;
bool Retain_200064703_1;
bool qosDelivered_200064703_1;
bool sendTrigOne_200064703_1;
bool sendTrigTwo_200064703_1;
uint16_t packetId_200064703_1;
String mqttServer;
int mqttPort;
String mqttUser;
String mqttPassword;
String mqttClientId;
bool mqttConnected;
String mqttInputTopic;
String mqttInputMessage;
AsyncMqttClient mqttClient;
Ticker mqttReconnectTimer;
WiFiEventHandler wifiConnectHandler;
WiFiEventHandler wifiDisconnectHandler;
uint16_t pubPacketId;
bool ESP8266ControllerWifiClient_HRD = 0;
bool ESP8266ControllerWifiClient_status = 1;
bool ESP8266ControllerWifiClient_isDHCP = 1;
bool ESP8266ControllerWifiClient_IsNeedReconect = 0;
bool ESP8266ControllerWifiClient_workStatus = 1;
char ESP8266ControllerWifiClient_SSID[40] = "Tenda_17E498"; // логин от wifi
char ESP8266ControllerWifiClient_password[40] = "makswhite"; // пароль от wifi
IPAddress ESP8266ControllerWifiClient_ip(0, 0, 0, 0);
IPAddress  ESP8266ControllerWifiClient_dns(0, 0, 0, 1);
IPAddress  ESP8266ControllerWifiClient_gateway(0, 0, 0, 1);
IPAddress ESP8266ControllerWifiClient_subnet(255, 255, 255, 0);
uint8_t ESP8266ControllerWifiClient_mac[6] = { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };
bool _bounseInputD5S = 0;
bool _bounseInputD5O = 0;
unsigned long _bounseInputD5P = 0UL;
String _swi1;
bool _trgs1 = 0;

void setup()
{
  WiFi.mode(WIFI_STA);
  _esp8266WifiModuleClientReconnect();
  pinMode(5, INPUT_PULLUP);
  pinMode(2, OUTPUT);

  _bounseInputD5O = digitalRead(5);


  wifiConnectHandler = WiFi.onStationModeGotIP(onWifiConnect);
  wifiDisconnectHandler = WiFi.onStationModeDisconnected(onWifiDisconnect);

  mqttClient.onMessage(messageReceived);
  mqttClient.onConnect(onMqttConnect);
  mqttClient.onDisconnect(onMqttDisconnect);
  mqttClient.onPublish(onMqttPublish);

  mqttClient.setWill("user_f8f027d8/espfaiz", 0, 0, "0"); // топик брокера это заменить на свой user_f8f027d8/espfaiz
  mqttClient.setKeepAlive(15);
  mqttClient.setCleanSession(0);
}

void loop()
{
  if (ESP8266ControllerWifiClient_IsNeedReconect) {
    _esp8266WifiModuleClientReconnect();
    ESP8266ControllerWifiClient_IsNeedReconect = 0;
  }
  ESP8266ControllerWifiClient_status = WiFi.status() == WL_CONNECTED;
  if (ESP8266ControllerWifiClient_status) {
    if (!ESP8266ControllerWifiClient_HRD) {
      ESP8266ControllerWifiClient_ip = WiFi.localIP();
      ESP8266ControllerWifiClient_subnet = WiFi.subnetMask();
      ESP8266ControllerWifiClient_gateway = WiFi.gatewayIP();
      ESP8266ControllerWifiClient_dns = WiFi.dnsIP();
      WiFi.macAddress(ESP8266ControllerWifiClient_mac);
      ESP8266ControllerWifiClient_HRD = 1;
    }
  }
  else {
    ESP8266ControllerWifiClient_HRD = 0;
  }

  bool  _bounceInputTmpD5 = (digitalRead(5));

  if (_bounseInputD5S)
  {
    if (millis() >= (_bounseInputD5P + 40))
    {
      _bounseInputD5O = _bounceInputTmpD5; _bounseInputD5S = 0;
    }
  }
  else
  {
    if (_bounceInputTmpD5 != _bounseInputD5O)
    {
      _bounseInputD5S = 1; _bounseInputD5P = millis();
    }
  }




  //Плата:1
  mqttServer = String("srv1.mqtt.4api.ru"); ип адрес брокера
  mqttPort = 9124; // порт брокера
  mqttUser = String("user_f8f027d8"); // имя пользователя брокера
  mqttPassword = String("pass_711732cf"); //пароль пользователя брокера
  mqttClientId = String("espfaiz"); // клиент ИД можно написать любой или оставить как есть

  Topic_95031111_1 = String("user_f8f027d8/espfaiz"); // топик брокера это заменить на свой user_f8f027d8/espfaiz
  QoS_95031111_1 = 0;
  Received_95031111_1 = false;
  if (mqttClient.connected()) {
    if (mqttInputTopic == Topic_95031111_1) {
      Received_95031111_1 = true;
      mqttInputTopic = "";
      Message_95031111_1 = mqttInputMessage;
    }
    if (!subscribed_95031111_1) {
      uint16_t packetId = mqttClient.subscribe(Topic_95031111_1.c_str(), QoS_95031111_1);
      if (packetId > 0) {
        subscribed_95031111_1 = true;
      }
    }
  }
  else {
    subscribed_95031111_1 = false;
  }
  if (((Message_95031111_1).equalsIgnoreCase(String("on")))) _trgs1 = 1;
  if (((Message_95031111_1).equalsIgnoreCase(String("off")))) _trgs1 = 0;
  digitalWrite(2, !(_trgs1));
  if (!(_bounseInputD5O))
  {
    _swi1 = String("command");
  } // команда command которая отправляется на брокера при нажатия на кнопку
  else
  {
    _swi1 = String("0");
  }
  Send_200064703_1 = ((_swi1).equalsIgnoreCase(String("command"))); // команда command которая отправляется на брокера при нажатия на кнопку
  Topic_200064703_1 = String("user_f8f027d8/espfaiz"); // топик брокера это заменить на свой user_f8f027d8/espfaiz
  Message_200064703_1 = _swi1;
  QoS_200064703_1 = 0;
  Retain_200064703_1 = 1;
  if (Send_200064703_1) {
    if (sendTrigTwo_200064703_1) {
      sendTrigOne_200064703_1 = 0;
    }
    else {
      sendTrigOne_200064703_1 = 1; sendTrigTwo_200064703_1 = 1;
    }
  }
  else {
    sendTrigOne_200064703_1 = 0; sendTrigTwo_200064703_1 = 0;
    qosDelivered_200064703_1 = 0;
  }
  if (sendTrigOne_200064703_1) {
    packetId_200064703_1 = mqttClient.publish(Topic_200064703_1.c_str(), QoS_200064703_1, Retain_200064703_1, Message_200064703_1.c_str());
  }
  if (packetId_200064703_1 > 1 && packetId_200064703_1 == pubPacketId) {
    qosDelivered_200064703_1 = 1;
    packetId_200064703_1 = 0;
  }
}

bool _isTimer(unsigned long startTime, unsigned long period)
{
  unsigned long currentTime;
  currentTime = millis();
  if (currentTime >= startTime) {
    return (currentTime >= (startTime + period));
  }
  else {
    return (currentTime >= (4294967295 - startTime + period));
  }
}

void messageReceived(char* topic, char* payload, AsyncMqttClientMessageProperties properties, size_t len, size_t index, size_t total)
{
  mqttInputTopic = topic;
  mqttInputMessage = "";
  for (size_t i = 0; i < len; i++) {
    mqttInputMessage += payload[i];
  }
}

void onWifiConnect(const WiFiEventStationModeGotIP& event)
{
  connectToMqtt();
}

void onWifiDisconnect(const WiFiEventStationModeDisconnected& event)
{
  mqttReconnectTimer.detach();
}

void connectToMqtt()
{
  mqttClient.setClientId(mqttClientId.c_str());
  mqttClient.setServer(mqttServer.c_str(), mqttPort);
  mqttClient.setCredentials(mqttUser.c_str(), mqttPassword.c_str());

  mqttClient.connect();
}

void onMqttDisconnect(AsyncMqttClientDisconnectReason reason)
{
  mqttConnected = false;
  if (WiFi.isConnected()) {
    mqttReconnectTimer.once(2, connectToMqtt);
  }
}

void onMqttConnect(bool sessionPresent)
{
  mqttClient.publish("user_f8f027d8/espfaiz", 0, 0, "1"); // топик брокера это заменить на свой user_f8f027d8/espfaiz
  mqttConnected = true;
}

void onMqttPublish(uint16_t packetId)
{
  pubPacketId = packetId;
}

int hexStrToInt(String instring)
{
  byte len = instring.length();
  if (len == 0) return 0;
  int result = 0;
  for (byte i = 0; i < 8; i++)   // только первые 8 цыфар влезуть в uint32
  {
    char ch = instring[i];
    if (ch == 0) break;
    result <<= 4;
    if (isdigit(ch))
      result = result | (ch - '0');
    else result = result | (ch - 'A' + 10);
  }
  return result;
}

void _esp8266WifiModuleClientReconnect()
{
  if (_checkMacAddres(ESP8266ControllerWifiClient_mac)) {
    wifi_set_macaddr(0, const_cast<uint8*>(ESP8266ControllerWifiClient_mac));
  }
  if (ESP8266ControllerWifiClient_isDHCP) {
    WiFi.config(0U, 0U, 0U, 0U, 0U);
  }
  else {
    WiFi.config(ESP8266ControllerWifiClient_ip, ESP8266ControllerWifiClient_gateway, ESP8266ControllerWifiClient_subnet, ESP8266ControllerWifiClient_dns, ESP8266ControllerWifiClient_dns);
  }
  WiFi.begin(ESP8266ControllerWifiClient_SSID, ESP8266ControllerWifiClient_password);
}

bool _checkMacAddres(byte array[])
{
  bool result = 0;
  for (byte i = 0; i < 6; i++)
  {
    if (array[i] == 255) {
      return 0;
    }
    if (array[i] > 0) {
      result = 1;
    }
  }
  return result;
}
