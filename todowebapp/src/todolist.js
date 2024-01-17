import './todolist.css';
import React, { useState, useEffect } from 'react';

function TodoList() {
  const [todos, setTodos] = useState([]);
  const [input, setInput] = useState('New Item');
  let currentdate=new Date();
  const [startDate, setStartDate] = useState(currentdate);
  const [endDate, setEndDate] = useState(currentdate.setHours(currentdate.getHours() + 1));

  useEffect(() => {
    fetch('http://localhost:5299/api/todo/items')
      .then(response => response.json())
      .then(data => {
        setTodos(data);
      });
  }, []);

  const handleAdd = () => {
    fetch('http://localhost:5299/api/todo/item', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ Title: input, StartDate: startDate,EndDate:endDate })
    })
      .then(response => response.json())
      .then(data => {
        setTodos([data,...todos ]);
        setInput('New Item');
        let currentnewDate=new Date();
        setStartDate(currentnewDate);
        setEndDate(currentnewDate.setHours(currentnewDate.getHours() + 1));
      });
  };

  const handleDelete = (todoItem) => {
    fetch(`http://localhost:5299/api/todo/item/`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ Id:todoItem.id, Title: todoItem.title, StartDate: todoItem.startDate,EndDate:todoItem.endDate })
    }).then(response => response.json())
      .then(data => {
        setTodos(data);
      });
  };

  const handleUpdate = (id, name) => {
    fetch(`http://localhost:5299/api/Todo/items/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ name })
    })
      .then(response => response.json())
      .then(data => {
        setTodos(todos.map(todo => todo.id === id ? data : todo));
      });
  };

  return (
    <div>
      <input value={input} onChange={e => setInput(e.target.value)} />
      <input type="datetime-local" value={startDate} onChange={e => setStartDate(e.target.value)} />
      <input type="datetime-local" value={endDate} onChange={e => setEndDate(e.target.value)} />
      <button onClick={handleAdd}>Add</button>
      <ul>
        {todos.map((todo) => (
          <li key={todo.id}>
              <div className="item-card">
                <strong>Title: </strong> {todo.title}<br/>
                <strong>Start Date: </strong> {new Date(todo.startDate).toLocaleString()}<br/>
                <strong>End Date: </strong> {new Date(todo.endDate).toLocaleString()}<br/>
                <button onClick={() => handleDelete(todo)}>Delete</button>
                <button onClick={() => handleUpdate(todo.id, 'New Name')}>Update</button>
              </div>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default TodoList;