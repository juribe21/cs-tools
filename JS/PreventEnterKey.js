/* Event on tool input */
onkeydown="event.key == 'Enter' && event.preventDefault()"

/* ******** */
 $(function () {
     $(document).ready(function () {
         $(function () {
             $("#tabs").tabs();
         });

         getCustomerName();
         //blockByUser();

         $("#EditCustomer").click(function () {
             redirect();
         });
     });

     /* Prevent Enter key */
     $(document).keypress(
         function (event) {
             if (event.which == '13') {
                 event.preventDefault();
             }
         });
 });