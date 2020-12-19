"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ImportaItau").build();

connection.on("ReceiveMessage", function (message) {
    var obj = JSON.parse(message);
  
    if (obj.Mensagem == "Fim") {
        alert("Importação concluída.");
    }
    document.getElementById("messagesList").innerHTML = obj.Mensagem;
    var element = document.getElementById("barra"); 

});
 

connection.start().then(function () {
    var li = document.createElement("li");
  
}).catch(function (err) {
    var li = document.createElement("li");
    li.textContent = err.toString();
   
});