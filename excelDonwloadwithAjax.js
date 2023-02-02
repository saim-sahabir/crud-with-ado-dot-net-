

$.ajax({
  url: "path/to/excel/file",
  method: "GET",
  xhrFields: {
    responseType: "blob"
  },
  success: function(data) {
    var a = document.createElement("a");
    var url = window.URL.createObjectURL(data);
    a.href = url;
    a.download = "file.xlsx";
    document.body.appendChild(a);
    a.click();
    setTimeout(function() {
      window.URL.revokeObjectURL(url);
    }, 100);
  }
});


