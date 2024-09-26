#include <SoftwareSerial.h>

SoftwareSerial BTSerial(10, 11); // RX, TX para el módulo Bluetooth

const int sensorPin = A0;  // Pin al que está conectado el sensor de temperatura TMP

void setup() {
  Serial.begin(9600); // Inicializar comunicación con el monitor serie
  BTSerial.begin(9600); // Inicializar comunicación con el HC-05

  Serial.println("HC-05 conectado. Leyendo temperatura...");
  BTSerial.println("HC-05 conectado. Leyendo temperatura...");
}

void loop() {
  // Leer el valor del sensor TMP
  int sensorValue = analogRead(sensorPin);

  // Convertir la lectura en voltaje (0-5V)
  float voltage = sensorValue * (5.0 / 1023.0);

  // Convertir el voltaje a temperatura en grados Celsius (TMP36)
  float temperatureC = (voltage - 0.5) * 100.0;

  // Enviar la temperatura por Bluetooth y Serial Monitor
  Serial.print("Temperatura: ");
  Serial.print(temperatureC);
  Serial.println(" °C");

  BTSerial.print("Temperatura: ");
  BTSerial.print(temperatureC);
  BTSerial.println(" °C");

  // Pausa de 1 segundo antes de la siguiente lectura
  delay(1000);
}