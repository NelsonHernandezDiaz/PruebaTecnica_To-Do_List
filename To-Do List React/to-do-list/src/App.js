import './App.css';
import ToDoListLogo from './imagenes/ToDoList-Logo.png';
import ListaTareas from './componentes/ListaTareas';

function App() {
  return (
    <div className='aplicacion-tareas'>
      <div className='ToDoList-Logo-contenedor'>
        <img 
        src={ToDoListLogo} 
        className='ToDoList-Logo' />
      </div>
      <div className='tareas-lista-principal'>
        <h1>TO-DO LIST</h1>
        <ListaTareas />
      </div>
    </div>
  );
}

export default App;
