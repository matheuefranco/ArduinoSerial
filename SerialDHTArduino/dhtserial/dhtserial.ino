#define ledPin 7
#define led 12
#include "dht.h"
int ledStatus;
const int pinoDHT11 = A2;
 
dht DHT; //VARIÁVEL DO TIPO DHT
 
void setup(){
  pinMode(led,OUTPUT);
  pinMode(ledPin, INPUT);
  Serial.begin(9600); 
}
 
void loop(){
  ledStatus = digitalRead(ledPin);
  //Serial.println(ledStatus);
  DHT.read11(pinoDHT11);
  if (Serial.available()) //se byte pronto para leitura
   {
    switch(Serial.read())      //verifica qual caracter recebido
    {
      case 'A':    if(ledStatus == 0){
                     digitalWrite(led,HIGH);
                     Serial.println("Acendeu");
                     }
      break;               
      case 'D':   if(ledStatus == 1){
                     digitalWrite(led,LOW); 
                     Serial.println("Apagou");
                  }
      break;            
      case 'T': Serial.println(DHT.temperature, 0); //IMPRIME NA SERIAL O VALOR DE TEMP MEDIDO E REMOVE A PARTE DECIMAL                     
      break;
      case 'U': Serial.println(DHT.humidity,0);
      break;
    }// fim switch
   }// fim if
  delay(500);
}
