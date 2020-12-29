//---------------------------------------------------------------
// Project: Virtuino MQTT ethernet example
// Created by Ilias Lamprou at Jan/16/2018
// MQTT Broker: shiftr.io. https://shiftr.io/try.
// MQTT library by Joël Gähwiler: https://github.com/256dpi/arduino-mqtt
//---------------------------------------------------------------

#include <Ethernet.h>

//---------- board settings ------------------
byte mac[] = {0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED};

//------ MQTT broker settings and topics
const char* broker = "broker.shiftr.io"; 
char mqttUserName[] = "virtuino";         
char mqttPass[] = "1234567890";               

//------ PIN settings and topics
#define MQTT_CONNECTION_LED_PIN 4  // MQTT connection indicator           
#define OUT1_PIN 5                 // Led or relay
#define OUT2_PIN 6                 // Led or relay
#define IN1_PIN 7                  // Button

const char* topic_pub_sensor1 = "sensor1"; // You can to replace the labels: sensor1, sensor2, input_1, etc. with yours
const char* topic_pub_sensor2 = "sensor2"; 

const char* topic_pub_in1 = "input_1"; 

const char* topic_sub_out1 = "output_1";
const char* topic_sub_out2 = "output_2";

const char* topic_sub_variable1 = "variable_1";

const unsigned long mqttPostingInterval = 5L * 1000L; // Post sensor data every 5 seconds.

//---------Variables and Libraries --------------------
#include <MQTTClient.h>
EthernetClient net;
MQTTClient client;

unsigned long lastUploadedTime = 0;
byte in1_lastState=2;
byte in2_lastState=2;

//========================================= connect
//=========================================
void connect() {   
 digitalWrite(MQTT_CONNECTION_LED_PIN,LOW);   // Turn off the MQTT connection LED
 Serial.print("\nConnecting...");
 //--- create a random client id
  char clientID[] ="VIRTUINO_0000000000";  // For random generation of client ID.
  for (int i = 9; i <19 ; i++) clientID[i]=  char(48+random(10));

  while (!client.connect("Arduino", mqttUserName, mqttPass)) {
    Serial.print(".");
    digitalWrite(MQTT_CONNECTION_LED_PIN,!digitalRead(MQTT_CONNECTION_LED_PIN));
    delay(1000);
  }
  Serial.println("\nconnected!");
  digitalWrite(MQTT_CONNECTION_LED_PIN,HIGH);

  client.subscribe(topic_sub_out1);
  client.subscribe(topic_sub_out2);
  client.subscribe(topic_sub_variable1);
  
  // client.unsubscribe(topic_sub_out2);
}


//========================================= messageReceived
//=========================================
void messageReceived(String &topic, String &payload) {
  Serial.println("incoming: " + topic + " - " + payload);
  if (topic==topic_sub_out1){
    int v = payload.toInt();
    if (v==1) digitalWrite(OUT1_PIN,HIGH);
    else digitalWrite(OUT1_PIN,LOW);
  }
  if (topic==topic_sub_out2){
    int v = payload.toInt();
    if (v==1) digitalWrite(OUT2_PIN,HIGH);
    else digitalWrite(OUT2_PIN,LOW);
  }

   if (topic==topic_sub_variable1){
    int v = payload.toInt();
    Serial.println("Variable 1 = "+String(v));
    // do something with the variable here
  }
  
}

//========================================= setup
//=========================================
//=========================================
void setup() {
  Serial.begin(9600);
  Serial.println("Setup");
  
  pinMode(MQTT_CONNECTION_LED_PIN,OUTPUT);
  pinMode(OUT1_PIN,OUTPUT); 
  pinMode(OUT2_PIN,OUTPUT); 
  pinMode(IN1_PIN,INPUT);
  
  Serial.begin(9600);
  Ethernet.begin(mac);
  client.begin(broker, net);
  client.onMessage(messageReceived);
  connect();
}
//========================================= loop
//=========================================
//=========================================
void loop() {
  client.loop();
  if (!client.connected())connect();

  //---- MQTT upload
  if (millis() - lastUploadedTime > mqttPostingInterval) {
    int sensor1_value =random(100);         // replace the random value with your sensor value
    client.publish(topic_pub_sensor1, String(sensor1_value),true,1);

    int sensor2_value = analogRead(A0);
    client.publish(topic_pub_sensor2, String(sensor2_value),true,1);  // upload the analog A0 value
   
    lastUploadedTime = millis();
  }


  //---- check if button is pushed
   byte input1_state = digitalRead(IN1_PIN);
   if (input1_state!=in1_lastState){
      client.publish(topic_pub_in1, String(input1_state),true,1);
      delay(100);
      in1_lastState=input1_state;
   // }
   }

  
}




