<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="King.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->
    <title>Bootstrap 101 Template</title>

    <!-- Bootstrap -->
    <link rel="stylesheet" href="http://cdn.bootcss.com/bootstrap/3.3.4/css/bootstrap.min.css">
</head>
<body>
<h3>图标</h3>
    <span class="glyphicon glyphicon-home"></span>
    <span class="glyphicon glyphicon-signal"></span>
    <span class="glyphicon glyphicon-cog"></span>
    <span class="glyphicon glyphicon-apple"></span>
    <span class="glyphicon glyphicon-trash"></span>
    <span class="glyphicon glyphicon-play-circle"></span>
    <span class="glyphicon glyphicon-headphones"></span>

    <h3>按钮</h3>
    <button type="button" class="btn btn-default">按钮</button>
    <button type="button" class="btn btn-primary">primary</button>
    <button type="button" class="btn btn-success">success</button>
    <button type="button" class="btn btn-info">info</button>
    <button type="button" class="btn btn-warning">warning</button>
    <button type="button" class="btn btn-danger">danger</button>

    <h3>按钮尺寸</h3>
    <button type="button" class="btn btn-default">按钮</button>
    <button type="button" class="btn btn-primary btn-lg">primary</button>
    <button type="button" class="btn btn-success btn-sm">success</button>
    <button type="button" class="btn btn-info btn-xs">info</button>

    <h3>把图标显示在按钮里</h3>
    <button type="button" class="btn btn-success"><span class="glyphicon glyphicon-home"></span>&nbsp;&nbsp;按钮</button>


    <h3>下拉菜单</h3>
    <div class="dropdown">
        <button class="btn btn-primary dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-expanded="true">
            Dropdown
        <span class="caret"></span>
        </button>
        <ul class="dropdown-menu" role="menu" aria-labelledby="dropdownMenu1">
            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Action</a></li>
            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Another action</a></li>
            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Something else here</a></li>
            <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Separated link</a></li>
        </ul>
    </div>


    <h3>输入框</h3>
    <div class="input-group">
        <span class="glyphicon glyphicon-user"></span>
        <input type="text" placeholder="username">
    </div>

    <div class="input-group">
        <span class="glyphicon glyphicon-lock"></span>
        <input type="password" placeholder="password">
    </div>


    <h3>导航栏</h3>
    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div id="navbar" class="navbar-collapse collapse">
            <ul class="nav navbar-nav">
                <li class="active"><a href="#">Home</a></li>
                <li><a href="#about">About</a></li>
                <li><a href="#contact">Contact</a></li>
                <li class="dropdown">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Dropdown <span class="caret"></span></a>
                    <ul class="dropdown-menu" role="menu">
                        <li><a href="#">Action</a></li>
                        <li><a href="#">Another action</a></li>
                        <li class="divider"></li>
                        <li class="dropdown-header">Nav header</li>
                        <li><a href="#">Separated link</a></li>
                    </ul>
                </li>
            </ul>
        </div>
        <!--/.nav-collapse -->
    </nav>


    <h3>表单</h3>
    <form>
        <div class="form-group">
            <span class="glyphicon glyphicon-user"></span>
            <input type="email" id="exampleInputEmail1" placeholder="Enter email">
        </div>
        <div class="form-group">
            <span class="glyphicon glyphicon-lock"></span>
            <input type="password" id="exampleInputPassword1" placeholder="Password">
        </div>
        <div class="form-group">
            <label for="exampleInputFile">File input</label>
            <input type="file" id="exampleInputFile">
            <p class="help-block">Example block-level help text here.</p>
        </div>
        <div class="checkbox">
            <label>
                <input type="checkbox">
                Check me out
            </label>
        </div>
        <button type="submit" class="btn btn-default">Submit</button>
    </form>



    <h3>警告框</h3>
    <div class="alert alert-warning alert-dismissible" role="alert">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <strong>Warning!</strong> Better check yourself, you're not looking too good.
    </div>
    <div class="alert alert-success" role="alert">
        <a href="#" class="alert-link">success!</a>
    </div>
    <div class="alert alert-info" role="alert">
        <a href="#" class="alert-link">info!</a>
    </div>
    <div class="alert alert-warning" role="alert">
        <a href="#" class="alert-link">warning!</a>
    </div>
    <div class="alert alert-danger" role="alert">
        <a href="#" class="alert-link">danger!</a>
    </div>



    <h3>进度条</h3>
    <div class="progress">
        <div class="progress-bar" role="progressbar" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100" style="width: 60%;">
            70%
        </div>
    </div>
    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="http://cdn.bootcss.com/jquery/1.11.2/jquery.min.js"></script>
    <!-- Include all compiled plugins (below), or include individual files as needed -->
    <script src="http://cdn.bootcss.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>
</body>
</html>
