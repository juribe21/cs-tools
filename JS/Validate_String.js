     /* Validate String */
     function ValidateString(textBoxId, DataType, NumberOfElements) {
         console.log(document.getElementById(textBoxId).value)
        
         if (FormatError == true && textBoxId != LabelError) {
             alert("Format Error Fix it Beofre Change Any other");
             document.getElementById(textBoxId).value = PreviewText;
             PreviewText = "";
         }
         else {
             var text = document.getElementById(textBoxId).value;
             PreviewText = text;

             switch (DataType) {
                 case "varchar":
                     //regular expression any character no more than NumberOfElements
                     if ((text.length < NumberOfElements) && (text.length > 0)) {
                         MarkTextBox(true, textBoxId);
                     }
                     else {
                         MarkTextBox(false, textBoxId);
                     }
                     break;
                 case "DropDown":
                     //do nothing as long as the values are pre defined
                     break;
                 case "ContactName":
                     //var regexName = /^(([A-Z][a-z]+)(\'[a-z])*(\ ){0,1}){2,3}$/
                     //var regexName = /^(([A-Z]((\' ){0,1}|[a-z]+))(\'[a-z]+)*(\ ){0,1}){2,3}$/
                     var regex = /^((([A-Z]((\'){0,1}|[a-z]+))(\'[a-z]+)*){1})((((\ ){1}[A-Z]((\' ){0,1}|[a-z]+))(\'[a-z]+)*){1,2})$/
                     var regex1 = regex.test(text);

                     if (regex1 == true && text.length <= NumberOfElements) {
                         MarkTextBox(true, textBoxId);
                     }
                     else {
                         MarkTextBox(false, textBoxId);
                     }
                     break;
                 case "PhoneNumber":
                     //var regex = /^(\+{1})([0-9]{1,2})((\ |\-)([0-9]{3})){2}((\ |\-)([0-9]{4})){1}$/
                     var regex = /^((\+{1})([0-9]{1,2})(\ )){0,1}(([0-9]{3})(\ |\-)){2}(([0-9]{4})){1}$/
                     var regex1 = regex.test(text);

                     if (regex1 == true && text.length <= NumberOfElements) {
                         MarkTextBox(true, textBoxId);
                     }
                     else {
                         MarkTextBox(false, textBoxId);
                     }
                     break;
                 case "Email":
                     var regex = /^[A-Za-z0-9]*((\.){0,1})(([A-Za-z0-9]*){0,1})(\@)([A-Za-z0-9]*)(.com|.net)$/
                     var regex1 = regex.test(text);

                     if (regex1 == true && text.length <= NumberOfElements) {
                         MarkTextBox(true, textBoxId);
                     }
                     else {
                         MarkTextBox(false, textBoxId);
                     }
                     break;
             }
         }
     }
