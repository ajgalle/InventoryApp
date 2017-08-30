using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace InventoryApp
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                // if user wants to add a new item not in database:

                // Add a photo, quantity, and name for part not yet in inventory.
                if (DropdownAddPartName.SelectedValue == "Other")
                {
                    if (FileUpload1.HasFile)
                    {
                        if (FileUpload1.PostedFile.ContentType.ToLower() == "image/jpeg" ||
                            FileUpload1.PostedFile.ContentType.ToLower()== "image/gif" ||
                            FileUpload1.PostedFile.ContentType.ToLower()=="image/jpg" ||
                            FileUpload1.PostedFile.ContentType.ToLower()=="image/png" ||
                            FileUpload1.PostedFile.ContentType.ToLower()=="image/x-png"||
                            FileUpload1.PostedFile.ContentType.ToLower()=="image/pjpeg")
                        {
                            if (FileUpload1.PostedFile.ContentLength < 102400)
                            {

                                FileUpload1.SaveAs(Server.MapPath("~/Images/" + FileUpload1.FileName));

                                lblStatusMessage.Text = "New inventory item created!";

                                string connString = ConfigurationManager.ConnectionStrings["dbInventoryAppConnectionString"].ConnectionString;

                                string queryString = "INSERT INTO tblInventory (PartName,QuantityInStock,ImagePath) VALUES (@PartName,@QuantityInStock,@ImagePath)";

                                using (SqlConnection conn = new SqlConnection(connString))
                                {
                                    using (SqlCommand cmd = new SqlCommand(queryString, conn))
                                    {
                                        conn.Open();

                                        // This is to avoid SQL Injection problems; we first declared @parameters in the command line, so these are adding to their values.

                                        cmd.Parameters.Add("@PartName", SqlDbType.NChar).Value = txtboxNewPartName.Text;
                                        cmd.Parameters.Add("@QuantityInStock", SqlDbType.Int).Value = Convert.ToInt32(txtboxQuantity.Text);
                                        cmd.Parameters.Add("@ImagePath", SqlDbType.NChar).Value = "~/Images/" + FileUpload1.FileName;

                                        cmd.ExecuteNonQuery();

                                        Response.Redirect("default.aspx");
                                    }
                                }
                            }
                            else
                            {
                                lblStatusMessage.Text = "Please make sure your image is less than 100KB.";
                                DropdownAddPartName.SelectedValue = "-1";

                            }
                        }
                        else
                        {
                            lblStatusMessage.Text = "Please only upload images. JPEGs with max width of 150px work best.";
                            DropdownAddPartName.SelectedValue = "-1";

                        }
                    }

                    else
                    {
                        lblStatusMessage.Text = "When adding a new inventory item, please select a image file. JPEGs with a max width of 150px and less than 100kb work best.";
                        DropdownAddPartName.SelectedValue = "-1";
                    }
                }
                else
                {
                    if (DropdownAddPartName.SelectedValue == "-1")
                    {
                        lblStatusMessage.Text = "Please select a part name first. If none exists, select 'other.'";
                    }
                    else
                    {
                        int toCompareNumber = Convert.ToInt32(txtboxQuantity.Text);
                        if (toCompareNumber > 0)
                        {
                            // So, only if you click add with a positive number of parts and a part name selected that already exisits will this part execute.
                            string connString = ConfigurationManager.ConnectionStrings["dbInventoryAppConnectionString"].ConnectionString;

                            string queryString = "UPDATE tblInventory SET QuantityInStock = QuantityInStock+@AddQuantity WHERE PartName=@PartName";

                            using (SqlConnection conn = new SqlConnection(connString))
                            {
                                using (SqlCommand cmd = new SqlCommand(queryString, conn))
                                {
                                    conn.Open();

                                    // This is to avoid SQL Injection problems; we first declared @parameters in the command line, so these are adding to their values.


                                    cmd.Parameters.Add("@AddQuantity", SqlDbType.Int).Value = Convert.ToInt32(txtboxQuantity.Text);
                                    cmd.Parameters.Add("@PartName", SqlDbType.NChar).Value = DropdownAddPartName.SelectedValue;
                                    cmd.ExecuteNonQuery();

                                    Response.Redirect("default.aspx");
                                }
                            }
                        }
                        else
                        {
                            lblStatusMessage.Text = "Please make sure you are adding a positive number of parts. If you need to remove parts from inventory, please use that form.";

                        }


                    }


                }
            }
        }

        protected void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string ToUpdate=dropdownPartNameInInventory.SelectedItem.Text;

                string connString = ConfigurationManager.ConnectionStrings["dbInventoryAppConnectionString"].ConnectionString;
                string queryString = "UPDATE tblInventory SET QuantityInStock = QuantityInStock - @Quantity WHERE PartName=@PartName";

                using (SqlConnection conn = new SqlConnection(connString))
                {
                   using (SqlCommand cmd = new SqlCommand(queryString, conn))
                    {
                        conn.Open();

                        cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Convert.ToInt32(txtboxWithdraw.Text);
                        cmd.Parameters.Add("@PartName", SqlDbType.NChar).Value = dropdownPartNameInInventory.Text;

                        cmd.ExecuteNonQuery();
                        lblStatusMessage.Text = "Parts successfully removed.";
                        GridView1.DataBind();

                    }
                }

            }
        }
    }
}


