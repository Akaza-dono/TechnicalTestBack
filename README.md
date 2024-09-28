1. Es necesario configurar el connectionstring tomando en cuanta los datos que aparecen dentro del mayor y menor'<>', validando que los datos sean correctos con respecto al servidor de bbdd
   ![alt text](https://github.com/Akaza-dono/images/blob/main/AppSettings.png?raw=true)<br>

2. Confirmar que el puerto para el front de Angular debe de ser 4200 ya que este esta directamente configurado en el startup de la aplicacion<br>
      ![alt text](https://github.com/Akaza-dono/images/blob/main/puertoFront.png?raw=true)<br>

3. Garantizar que el proytecto de inicio sea el que aparece en la imagen seleccionado
      ![alt text](https://github.com/Akaza-dono/images/blob/main/proyecto.png?raw=true)<br>

4. Para Autenticarse con Swagger dan clic en la siguiente opcion<br>
      ![alt text](https://github.com/Akaza-dono/images/blob/main/Autorice.png?raw=true)<br>
   y en la ventana que aparece pegan el token generado en api/login el cual les permitira crear un token con diferentes permisos, User: Admin, password: Admin o User: Noob, password: Noob<br>
   ![alt text](https://github.com/Akaza-dono/images/blob/main/GeneracionLogToken.png?raw=true)<br>
   De esta forma ya todos los endpoints tienen de referencia ese token para la autenticacion
