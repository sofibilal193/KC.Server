/* eslint-disable no-unused-vars */
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './Pages/HomePage';
import UsersList from './components/UsersList';
import Register from './components/Register'; 
import Login from './components/Login';
import RouterConfig from './Routes/Routes';

function App() {
    return (

     <div className="App">
      <RouterConfig />
    </div>

    );
}

export default App;

