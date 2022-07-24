import React, { useState } from 'react';
import TareaFormulario from './TareaFormulario';
import Tarea from './Tarea';
import '../hojas-de-estilo/ListaTareas.css';

function ListaTareas() {  

  const [tareas, setTareas] = useState([]);
  const [modoEditar, setModoEditar] = useState(false);
  const [id, setId] = useState("");

  const agregarTarea = tarea => {    
    if (tarea.texto.trim()) {
      tarea.texto = tarea.texto.trim();
      const tareasActualizadas = [tarea, ...tareas];
      setTareas(tareasActualizadas);
    }
  }

  const editarTarea = tarea => {
    setTareas(tarea.texto);
    setModoEditar(true);
    setId(tarea.id);
    const tareasActualizadas = tareas.map(tarea => 
      tarea.id === id?{texto : tarea.texto} : tarea.texto)
    setTareas(tareasActualizadas);
  }

  const eliminarTarea = id => {
    const tareasActualizadas = tareas.filter(tarea => tarea.id !== id);
    setTareas(tareasActualizadas);
  }

  const completarTarea = id => {
    const tareasActualizadas = tareas.map(tarea => {
      if (tarea.id === id) {
        tarea.completada = !tarea.completada;
      }
      return tarea;
    });
    setTareas(tareasActualizadas);
  }
  
  return (
    <>
      <TareaFormulario onSubmit={modoEditar? editarTarea : agregarTarea} />
      <div className='tareas-lista-contenedor'>
        {
          tareas.map((tarea) =>
            <Tarea
              key={tarea.id}
              id={tarea.id} 
              texto={tarea.texto}
              completada={tarea.completada}
              completarTarea={completarTarea}
              editarTarea={editarTarea}
              eliminarTarea={eliminarTarea} />
          ) 
        }
      </div>
    </>
  );    
}

export default ListaTareas;