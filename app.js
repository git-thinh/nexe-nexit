var http = require('http');
http.createServer(function (req, res) {
  res.write(new Date().toString());
  res.end();
}).listen(6369);