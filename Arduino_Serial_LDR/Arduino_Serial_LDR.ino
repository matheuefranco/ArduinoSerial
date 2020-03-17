int ledPin = 13; //Led no pino 13
int ldrPin = A0; //LDR no pino analígico 0
int ldrValor = 0; //Valor lido do LDR
 
void setup() {
 pinMode(ledPin,OUTPUT); //define a porta 7 como saída
 Serial.begin(9600); //Inicia a comunicação serial
}
 
void loop() {
 ///ler o valor do LDR
 ldrValor = analogRead(ldrPin); //O valor lido será entre 0 e 1023
 if (Serial.available()) //se byte pronto para leitura
   {
    switch(Serial.read())      //verifica qual caracter recebido
    {
      case 'A': //caso 'A'
            Serial.println(ldrValor);  
      break;
    }// fim switch
   }// fim if

 //se o valor lido for maior que 500, liga o led
 if (ldrValor<600 ) 
  digitalWrite(ledPin,HIGH);
 else digitalWrite(ledPin,LOW);
 delay(100);
}
