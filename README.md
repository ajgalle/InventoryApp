# InventoryApp
A web form with a masterpage made in Visual Studio Community 2017 and SQL Server 2014. jQuery, C#, ASP.net, ADO.net, bootstrap, and more!

What is this?

This inventory control web app is a simple web form with a master page. Imagine working in a warehouse where shipments come in and out all day.  When shipments come in, you have to update what came in and how much, and from time to time will have to add new items you haven't received before. When shipments go out, you'll have to indicate how much has went out.  This app allows negative numbers to reflect when orders come in that exceed on-hand amounts. That is, a negative number indicates a backorder situation.

What's going on under the hood?

    The app has been made mobile responsive using bootstrap and its client side functionality is powered by jQuery.
    
    The server side code is a mix of C# and ADO.net.
    
    It uses a FileUpload control to allow user uploads of small JPEGs after a few rounds of validation occurs (controlling for file type, size, etc.)
    
    It uses ADO.net commands to communicate to a MS SQL server. Each command incorporates SQL injection defense strategies.

    The inventory view itself is created dynamically using a datagrid control with its default formatting and columns ripped out to offer a more visually pleasing experience.
 
 Thank you so much for checking it out!
