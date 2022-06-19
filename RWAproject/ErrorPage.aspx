<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="RWAproject.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
    <style>
        * {
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            box-sizing: border-box;
            font-family: sans-serif;
        }

        .buttons {
            margin: 10%;
            text-align: center;
        }

        .btn-hover {
            font-size: 1.2em;
            font-weight: 400;
            color: #fff;
            cursor: pointer;
            margin: 1em;
            padding: .5em 1.5em;
            text-align: center;
            border: none;
            background-size: 300% 100%;
            border-radius: 1em;
            moz-transition: all .4s ease-in-out;
            -o-transition: all .4s ease-in-out;
            -webkit-transition: all .4s ease-in-out;
            transition: all .4s ease-in-out;
        }

            .btn-hover:hover {
                background-position: 100% 0;
                moz-transition: all .4s ease-in-out;
                -o-transition: all .4s ease-in-out;
                -webkit-transition: all .4s ease-in-out;
                transition: all .4s ease-in-out;
            }

            .btn-hover:focus {
                outline: none;
            }

            .btn-hover.color-9 {
                background-image: linear-gradient(to right, #25aae1, #4481eb, #04befe, #3f86ed);
                box-shadow: 0 4px 15px 0 rgba(65, 132, 234, 0.75);
            }
    </style>
</head>
<body style="background-color: #e3f2fd">
    <form id="form1" runat="server">
        <main style="display: flex; justify-content: center; align-items: center; height: 100vh;">
            <div style="width: 30em; background-color: white; height: 25em; text-align: center; box-shadow: rgba(0, 0, 0, 0.25) 0px 54px 55px, rgba(0, 0, 0, 0.12) 0px -12px 30px, rgba(0, 0, 0, 0.12) 0px 4px 6px, rgba(0, 0, 0, 0.17) 0px 12px 13px, rgba(0, 0, 0, 0.09) 0px -3px 5px; border-radius: 1.5em">
                <h1 style="font-size: 5em; margin-bottom: 0">404</h1>
                <h3>Page not found</h3>
                <p>
                    An error occurred while loading this page.
                    <br />
                    Please try again.
                </p>
                <div class="button">
                    <asp:Button class="btn-hover color-9" ID="BtnTryAgain" runat="server" Text="Try again" OnClick="BtnTryAgain_Click"/>
                </div>
            </div>
        </main>
    </form>
</body>
</html>
