function loguear() {
  let user = document.getElementById("usuario").value;
  let pass = document.getElementById("clave").value;
  if (user == "promerica" && pass == "12345678") {
    alert("Se ha logueado exitosamente");
  } else alert("Las credenciales son incorrectas");
}
