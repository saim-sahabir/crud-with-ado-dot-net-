

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



let xhr = new XMLHttpRequest();
xhr.open("GET", "path/to/excel/file", true);
xhr.responseType = "blob";
xhr.onload = function(e) {
  if (this.status === 200) {
    let blob = new Blob([this.response], { type: "application/vnd.ms-excel" });
    let link = document.createElement("a");
    link.href = window.URL.createObjectURL(blob);
    link.download = "file.xls";
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }
};
xhr.send();


