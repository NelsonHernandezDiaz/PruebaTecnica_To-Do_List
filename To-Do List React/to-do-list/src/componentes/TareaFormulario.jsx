import React, { useState } from 'react';
import '../hojas-de-estilo/TareaFormulario.css';
import { v4 as uuidv4 } from 'uuid';

function TareaFormulario(props) {  

  const [input, setInput] = useState('');
  const [modoEditar, setModoEditar] = useState(false);

  const manejarCambio = e => {
    setInput(e.target.value);
  }

  const manejarEnvio = e => {
    e.preventDefault();    
    const tareaNueva = {
      id: uuidv4(),
      texto: input,
      estado: true
    }
    props.onSubmit(tareaNueva);
  }

  const manejarEdicion = e => {
    e.preventDefault();    
    setModoEditar(true);
    const tareaEditada = {
      id: e.target.value,
      texto: input,
      estado: true
    }
    props.onSubmit(tareaEditada);
  }

  return (
    <form 
      className='tarea-formulario'
      onSubmit={modoEditar? manejarEdicion : manejarEnvio}>
      <input 
        className='tarea-input'
        type='text'
        placeholder='Escribe una Tarea'
        name='texto'
        onChange={manejarCambio}
      />
      <button className={modoEditar? 'tarea-boton-guardar' : 'tarea-boton-agregar'}>        
        {modoEditar? 'Guardar' : 'Agregar'}
      </button>
    </form>
  );
}

export default TareaFormulario;