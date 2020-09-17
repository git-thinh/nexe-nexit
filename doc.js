const PATH = require('path');

const docx = require('./docx.js');

const file = PATH.join(__dirname, 'test/HTTD.docx');
console.log(file);

docx.extract(file).then(function(res, err) {
    if (err) {
        console.log(err)
    }
    console.log(res)
})