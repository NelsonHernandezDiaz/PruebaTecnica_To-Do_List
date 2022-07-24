import React, { useState } from 'react';
import '../hojas-de-estilo/Tarea.css';
import { AiOutlineEdit } from "react-icons/ai";
import { AiOutlineCloseCircle } from "react-icons/ai";

function Tarea({ id, texto, completada, completarTarea, editarTarea, eliminarTarea }) {  
    return (
      <div className={completada ? 'tarea-contenedor completada' : 'tarea-contenedor'}>
        <div 
          className='tarea-texto'
          onClick={() => completarTarea(id)}>
          {texto}
        </div>
        <div 
          className='tarea-contenedor-iconos'
          onClick={() => editarTarea(true)}>
          <AiOutlineEdit className='tarea-icono' />
        </div>
        <div 
          className='tarea-contenedor-iconos'
          onClick={() => eliminarTarea(id)}>
          <AiOutlineCloseCircle className='tarea-icono' />
        </div>
      </div>
    );    
  }

export default Tarea;