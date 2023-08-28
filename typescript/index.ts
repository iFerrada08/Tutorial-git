let track = "123";
const t: number = 1;
console.info("El valor de t es ", t);

interface ITrack {
    trackId: number;
    name: string;
    duration: number;
    enabled: boolean;
    // opcional
    artistId?: number;
}

const trackObj: ITrack = {
    trackId: 1,
    name: "Cancion 1",
    duration: 3000,
    enabled: false,
    // artistId: 1234
}

console.info("trackObj", trackObj);

function comprar(track: ITrack) {
    console.info("Se esta comprando el track con ID", track.trackId);
}

// Ejecutar la funcion
comprar(trackObj);