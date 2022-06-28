//---------------------------------------------------------------
// Project: Virtuino MQTT ethernet example
// Created by Bloodies
// MQTT Broker: shiftr.io. https://shiftr.io/try.
// MQTT library by Joël Gähwiler: https://github.com/256dpi/arduino-mqtt
//---------------------------------------------------------------

//#include <AsyncMqttClient.h>
//#include <EEPROM.h>
//#include <ArduinoMqttClient.h>
#include <Ethernet.h>
#include <MQTTClient.h>
#include <ArduinoJson.h>

//----------------- board settings -----------------
IPAddress ip(192,168,0,133);
byte mac[] = {0x24, 0xD2, 0x78, 0x5B, 0x68, 0xFF};
char guid[] = "5473f20f-05de-4e75-91af-686490030c38";

EthernetClient net;
MQTTClient client;

unsigned long lastUploadedTime = 0;
byte in1_lastState = 2;
byte in2_lastState = 2;

//------------- MQTT settings and topics------------
const char* broker  = "ems.insyte.ru";
int         port    = 1883;
char mqttUserName[] = "admin@ems.ru";
char mqttPassword[] = "Admin!23";

const char* attributes_in            = "/attributes/5473f20f-05de-4e75-91af-686490030c38/in/#";
const char* variables_in             = "/variables/5473f20f-05de-4e75-91af-686490030c38/in/#";

const char* attributes_out_state     = "/attributes/5473f20f-05de-4e75-91af-686490030c38/out/state";
const char* attributes_out_variables = "/attributes/5473f20f-05de-4e75-91af-686490030c38/out/variables";
const char* attributes_out_right     = "/attributes/5473f20f-05de-4e75-91af-686490030c38/out/sending_right";
const char* attributes_out_timeout   = "/attributes/5473f20f-05de-4e75-91af-686490030c38/out/sending_timeout";
const char* attributes_out_keeping   = "/attributes/5473f20f-05de-4e75-91af-686490030c38/out/keeping_right";

const unsigned long mqttPostingInterval = 5L * 1000L; // Post sensor data every 5 seconds.

//--------------------- JSON ---------------------


//------------------ PIN settings ------------------
#define MQTT_CONNECTION_LED_PIN 4  // MQTT connection indicator           
#define OUT1_PIN 5                 // Led or relay
#define OUT2_PIN 6                 // Led or relay
#define IN1_PIN 7                  // Button

void connect() {
  digitalWrite(MQTT_CONNECTION_LED_PIN, LOW);  // Turn off the MQTT connection LED
  Serial.print("\nConnecting.");
  //--- create a random client id
  char clientID[] = "ARDUINO_0000000000";  // For random generation of client ID.
  for (int i = 9; i < 19 ; i++) clientID[i] =  char(48 + random(10));

  while (!client.connect("Arduino", mqttUserName, mqttPassword)) {
    Serial.print(".");
    digitalWrite(MQTT_CONNECTION_LED_PIN, !digitalRead(MQTT_CONNECTION_LED_PIN));
    delay(1000);
  }
  Serial.println("\nconnected!");
  digitalWrite(MQTT_CONNECTION_LED_PIN, HIGH);

  client.subscribe(attributes_in);
  Serial.println("attributes_in sub");
  client.subscribe(variables_in);
  Serial.println("variables_in sub");
  // client.unsubscribe(topic_sub_out2);
}

//----------------- board settings -----------------
// put your setup code here, to run once:
void setup()
{
  Serial.begin(9600);
  Serial.println("Setup");

  pinMode(MQTT_CONNECTION_LED_PIN, OUTPUT);
  pinMode(OUT1_PIN, OUTPUT);
  pinMode(OUT2_PIN, OUTPUT);
  pinMode(IN1_PIN, INPUT);

  Serial.begin(9600);
  Ethernet.begin(mac);
  client.begin(broker, net);
  //client.publish(topic_pub_pingserv, "PING");
  Serial.println("output -> PING");
  client.onMessage(messageReceived);
  connect();
}
//------------------ main program ------------------
// put your main code here, to run repeatedly:
void loop()
{
  client.loop();
  if (!client.connected())connect();

  //---- MQTT upload
  //if (millis() - lastUploadedTime > mqttPostingInterval)
  //{
  //  int sensor1_value = random(100);        // replace the random value with your sensor value
  //  client.publish(topic_pub_sensor1, String(sensor1_value), true, 1);

  //  int sensor2_value = analogRead(A0);
  //  client.publish(topic_pub_sensor2, String(sensor2_value), true, 1); // upload the analog A0 value

  //  lastUploadedTime = millis();
  //}
  //---- check if button is pushed
  byte input1_state = digitalRead(IN1_PIN);
  //if (input1_state != in1_lastState)
  //{
  //  client.publish(topic_pub_in1, String(input1_state), true, 1);
  //  delay(100);
  //  in1_lastState = input1_state;
  //}
}
//------------------ main program ------------------

void messageReceived(String &topic, String &payload)
{
  char RegistrationModel = "{";
  RegistrationModel += "\"GUID\":\"";
  RegistrationModel += "5473f20f-05de-4e75-91af-686490030c38";
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
  RegistrationModel += "192.168.0.133";
  RegistrationModel += "\",\"gateway\":\"";
  RegistrationModel += "192.168.0.133";
  RegistrationModel += "\",\"mac\":\"";
  RegistrationModel += "24:D2:78:5B:68:FF";
  RegistrationModel += "\",\"timestamp\":\"";
  RegistrationModel += "2020-05-05T13:04:04Z";
  RegistrationModel += "\",\"Internet_channel\":\"";
  RegistrationModel += "ETH";
  RegistrationModel += "\"}";
  
  float battary = 100.00;
  String DeviceStatusModel = "{";
  DeviceStatusModel += "\"state\":\"";
  DeviceStatusModel += "enabled";
  DeviceStatusModel += "\",\"heath\":\"";
  DeviceStatusModel += "ok";
  DeviceStatusModel += "\",\"power\":\"";
  DeviceStatusModel += "battery";
  DeviceStatusModel += "\",\"battery\":\"";
  DeviceStatusModel += battary;
  DeviceStatusModel += "\",\"Internet_channel\":\"";
  DeviceStatusModel += "ETH";
  DeviceStatusModel += "\"}";
  
  int id_var = 1;
  bool enabled_var = true;
  int timeout_var = 3000;
  String VariableStatusModel = "{";
  VariableStatusModel += "\"id\":\"";
  VariableStatusModel += id_var;
  VariableStatusModel += "\",\"name\":\"";
  VariableStatusModel += "light";
  VariableStatusModel += "\",\"type\":\"";
  VariableStatusModel += "IN";
  VariableStatusModel += "\",\"data_type\":\"";
  VariableStatusModel += "STRING";
  VariableStatusModel += "\",\"enabled\":\"";
  VariableStatusModel += enabled_var;
  VariableStatusModel += "\",\"timeout\":\"";
  VariableStatusModel += timeout_var;
  VariableStatusModel += "\"}";

  int id_conf = 1;
  bool enabled_conf = true;
  int timeout_conf = 3000;
  bool section_conf = true;
  String DeviceVariablesConfig  = "{";
  DeviceVariablesConfig += "\"id\":\"";
  DeviceVariablesConfig += id_conf;
  DeviceVariablesConfig += "\",\"enabled\":\"";
  DeviceVariablesConfig += enabled_conf;
  DeviceVariablesConfig += "\",\"timeout\":\"";
  DeviceVariablesConfig += timeout_conf;
  DeviceVariablesConfig += "\",\"section\":\"";
  DeviceVariablesConfig += section_conf;
  DeviceVariablesConfig += "\"}";

  int id_data = 1;
  String VariableDataName = "{";
  VariableDataName += "\"id\":\"";
  VariableDataName += id_data;
  VariableDataName += "\",\"timestamp\":\"";
  VariableDataName += "2020-05-05T13:04:04Z";
  VariableDataName += "\",\"value\":\"";
  VariableDataName += "105.2";
  VariableDataName += "\"}";
  
  Serial.println("incoming: " + topic + " - " + payload);
  if (topic == attributes_in)
  /*if (topic == topic_sub_pingserv)    //recieving from server/cmd/pingstatus/<GUID>
  {
    String text = payload;
    if (text == "PONG")
    {
      Serial.println("Success");
      client.onMessage(messageReceived);
      client.publish(topic_pub_reg, RegistrationModel);
    }
    else
    {
      Serial.println("no answer");
      delay(1000); //таймер на 10 мин
      client.publish(topic_pub_pingserv, "PING");
      client.onMessage(messageReceived);
    }
  }
  if (topic == topic_sub_reg)         //recieving from server/registration/status/<GUID>
  {
    String text = payload;
    if (text == "SUCCESS")
    {
      Serial.println("Success");
      client.onMessage(messageReceived);
    }
    else if (text == "ALREDY_EXIST")
    {
      Serial.println("ALREDY_EXIST");
      client.onMessage(messageReceived);
    }
    else if (text == "ACCESS_DENIED")
    {
      Serial.println("ACCESS_DENIED");
      client.publish(topic_pub_pingserv, "PING");
      client.onMessage(messageReceived);
    }
    else if (text == "INTERNAL_ERROR")
    {
      Serial.println("INTERNAL_ERROR");
      client.publish(topic_pub_pingserv, "PING");
      client.onMessage(messageReceived);
    }
    else
    {
      delay(1000);
      client.publish(topic_pub_pingserv, "PING");
      client.onMessage(messageReceived);
    }
  }
  if (topic == topic_sub_devstatus)   //recieving from device/cmd/<GUID>
  {
    String text = payload;
    if (text == "REQ DEV STATUS ")
    {
      client.publish(topic_pub_devstatus, DeviceStatusModel);
    }
  }
  if (topic == topic_sub_variables)   //recieving from device/cmd/<GUID>
  {
    String text = payload;
    if (text == "REQ DEV VAR STATUS ")
    {
      client.publish(topic_pub_devstatus, VariableStatusModel);
    }
  }
  if (topic == topic_sub_config)      //recieving from device/variables/config/<GUID>
  {
    String text = payload;
    if (text == "")
    {
    }
    else
    {
    }
  }
  if (topic == topic_sub_vardata)     //recieving from server/status/<GUID>
  {
    String text = payload;
    if (text == "")
    {
    }
    else
    {
    }
  }
  if (topic == topic_sub_pingdev)     //recieving from device/cmd/<GUID>
  {
    String text = payload;
    if (text == "")
    {
    }
    else
    {
    }
  }
  */
  }
}
