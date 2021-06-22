const express = require('express');
const router = express.Router();
var Ddos = require('ddos');
var ddos = new Ddos({burst:10, limit:15})
router.use(ddos.express);

router.get('/', (req, res) => res.render('welcome'));

module.exports = router;
