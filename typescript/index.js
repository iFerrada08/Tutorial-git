var track = "123";
var t = 1;
console.info("El valor de t es ", t);
var trackObj = {
    trackId: 1,
    name: "Cancion 1",
    duration: 3000,
    enabled: false
};
console.info("trackObj", trackObj);
function comprar(track) {
    console.info("Se esta comprando el track con ID", track.trackId);
}
// Ejecutar la funcion
comprar(trackObj);
