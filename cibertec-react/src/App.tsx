import React from 'react';
import "bootstrap/dist/css/bootstrap.min.css";
//import "./App.css";
import Home from './Components/Home';
import Canciones from './Components/Canciones';
import Login from './Components/Login';
import { IUser } from './Components/_types';
import Chat from './Components/Chat';

function App() {
  // Crear una variable de estado
  const [vista, setVista] = React.useState("login");
  const [user, setUser] = React.useState<IUser>();
  return (
    <div className="App">
      {vista === "chat" && <Chat></Chat>}
      {vista === "login" && <Login actualizarUsuario={(newUser: IUser) => {
        setUser(newUser);
      }} actualizarVista={(nuevaVista: string) => { setVista(nuevaVista); }} />}
      {vista === "home" && <Home user={user} actualizarVista={(nuevaVista: string) => { setVista(nuevaVista); }} />}
      {vista === "canciones" && <Canciones></Canciones>}
    </div>
  );
}

export default App;
