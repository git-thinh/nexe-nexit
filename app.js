var http = require('http');
http.createServer(function (req, res) {
    res.write(process.env.PORT_REDIS + ' = ' + new Date().toString());
  res.end();
}).listen(12345);