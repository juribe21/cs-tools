
/* Create listener for <select> into (document).ready block */
/* (e) represents the file to be uplaoded */

$(document).ready(function () {
  $("#fileInput").on("change", function () {
    var file = this.files[0];
    var reader = new FileReader();

    reader.onload = function (e) {
      var base64String = e.target.result; // → This will be the Base64 encoded file data

      $.ajax({
        type: "POST",
        url: "YourPage.aspx/UploadFile", // Replace with your WebMethod URL
        data: JSON.stringify({ fileName: file.name, fileData: base64String }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
          console.log("File uploaded successfully:", response.d);
        },
        error: function (xhr, status, error) {
          console.error("Error uploading file:", error);
        },
      });
    };

    reader.readAsDataURL(file); // Read the file as a data URL (Base64)
  });
});
