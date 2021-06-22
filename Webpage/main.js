const express = require('express');
var Ddos = require('ddos');
const expressLayouts = require('express-ejs-layouts');
const flash = require('connect-flash');
const path = require('path');
var ddos = new Ddos({burst:10, limit:15})

const app = express();


// EJS
app.use(expressLayouts);
app.use(ddos.express);
app.set('view engine', 'ejs');

// Express body parser
app.use(express.urlencoded({ extended: true }));

// Connect flash
app.use(flash());
app.use(express.static(path.join(__dirname ,'assets')));
console.log(path.join(__dirname ,'assets'));


// Routes
app.use('/', require('./routes/index.js'));

const PORT = 4567;

app.listen(PORT, console.log(`Server running on  http://localhost:${PORT}/`));
