const express = require('express');
const app = express();
const port = 12345;

const url = require('url');
const querystring = require('querystring');
const path = require('path');

const docx = require('./docx.js');


app.get('/', (req, res) => {
    res.send(new Date().toString());
});

app.get('/docx/:fileName', async function (req, res) {
    const fileName = req.params.fileName;
    const file = path.join(__dirname, 'test/' + fileName + '.docx');
    console.log(file);

    docx.extract(file).then(function (text, err) {
        if (err) {
            console.log(err)
        }
        const data = text.split('^').join('\r\n\r\n');
        //console.log(text)
        res.type('text/plain');
        res.send(data);
    })
});


app.listen(port, () => {
    console.log(`Example app listening at http://localhost:${port}`)
});