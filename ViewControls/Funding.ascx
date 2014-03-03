<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Funding.ascx.vb" Inherits="Connect.Modules.Kickstart.Funding" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.UI.WebControls" Assembly="DotNetNuke.Web" %>

	<style>
		.meter { 
			height: 20px;  /* Can be anything */
			position: relative;
			margin: 20px 0px 20px 0px; /* Just for demo spacing */
			background: #fefefe;
			-moz-border-radius: 0px;
			-webkit-border-radius: 0px;
			border-radius: 0px;
			padding: 5px;
			-webkit-box-shadow: inset 0 -1px 1px rgba(255,255,255,0.3);
			-moz-box-shadow   : inset 0 -1px 1px rgba(255,255,255,0.3);
			box-shadow        : inset 0 -1px 1px rgba(255,255,255,0.3);
		}
		.meter > span {
			display: block;
			height: 100%;
			   -webkit-border-top-right-radius: 0px;
			-webkit-border-bottom-right-radius: 0px;
			       -moz-border-radius-topright: 0px;
			    -moz-border-radius-bottomright: 0px;
			           border-top-right-radius: 0px;
			        border-bottom-right-radius: 0px;
			    -webkit-border-top-left-radius: 0px;
			 -webkit-border-bottom-left-radius: 0px;
			        -moz-border-radius-topleft: 0px;
			     -moz-border-radius-bottomleft: 0px;
			            border-top-left-radius: 0px;
			         border-bottom-left-radius: 0px;
			background-color: rgb(43,194,83);
			background-image: -webkit-gradient(
			  linear,
			  left bottom,
			  left top,
			  color-stop(0, rgb(43,194,83)),
			  color-stop(1, rgb(84,240,84))
			 );
			background-image: -moz-linear-gradient(
			  center bottom,
			  rgb(43,194,83) 37%,
			  rgb(84,240,84) 69%
			 );
			-webkit-box-shadow: 
			  inset 0 2px 9px  rgba(255,255,255,0.3),
			  inset 0 -2px 6px rgba(0,0,0,0.4);
			-moz-box-shadow: 
			  inset 0 2px 9px  rgba(255,255,255,0.3),
			  inset 0 -2px 6px rgba(0,0,0,0.4);
			box-shadow: 
			  inset 0 2px 9px  rgba(255,255,255,0.3),
			  inset 0 -2px 6px rgba(0,0,0,0.4);
			position: relative;
			overflow: hidden;
		}
		.meter > span:after, .animate > span > span {
			content: "";
			position: absolute;
			top: 0; left: 0; bottom: 0; right: 0;
			background-image: 
			   -webkit-gradient(linear, 0 0, 100% 100%, 
			      color-stop(.25, rgba(255, 255, 255, .2)), 
			      color-stop(.25, transparent), color-stop(.5, transparent), 
			      color-stop(.5, rgba(255, 255, 255, .2)), 
			      color-stop(.75, rgba(255, 255, 255, .2)), 
			      color-stop(.75, transparent), to(transparent)
			   );
			background-image: 
				-moz-linear-gradient(
				  -45deg, 
			      rgba(255, 255, 255, .2) 25%, 
			      transparent 25%, 
			      transparent 50%, 
			      rgba(255, 255, 255, .2) 50%, 
			      rgba(255, 255, 255, .2) 75%, 
			      transparent 75%, 
			      transparent
			   );
			z-index: 1;
			-webkit-background-size: 50px 50px;
			-moz-background-size: 50px 50px;
			background-size: 50px 50px;
			-webkit-animation: move 2s linear infinite;
			-moz-animation: move 2s linear infinite;
			   -webkit-border-top-right-radius: 8px;
			-webkit-border-bottom-right-radius: 8px;
			       -moz-border-radius-topright: 8px;
			    -moz-border-radius-bottomright: 8px;
			           border-top-right-radius: 8px;
			        border-bottom-right-radius: 8px;
			    -webkit-border-top-left-radius: 20px;
			 -webkit-border-bottom-left-radius: 20px;
			        -moz-border-radius-topleft: 20px;
			     -moz-border-radius-bottomleft: 20px;
			            border-top-left-radius: 20px;
			         border-bottom-left-radius: 20px;
			overflow: hidden;
		}
		
		.animate > span:after {
			display: none;
		}
		
		@-webkit-keyframes move {
		    0% {
		       background-position: 0 0;
		    }
		    100% {
		       background-position: 50px 50px;
		    }
		}
		
		@-moz-keyframes move {
		    0% {
		       background-position: 0 0;
		    }
		    100% {
		       background-position: 50px 50px;
		    }
		}
		
		
		
		.nostripes > span > span, .nostripes > span:after {
			-webkit-animation: none;
			-moz-animation: none;
			background-image: none;
		}
	</style>

<div class="kickstart-projectmeter dnnClear">
    
    <h3><asp:Literal ID="lblFundingHead" runat="server"></asp:Literal></h3>

    <div class="funding-needed">
        <span class="funding-needed"><asp:Literal ID="lblFundingNeeded" runat="server"></asp:Literal></span>
    </div>
    <div class="funding-reached">
        <span class="funding-reached"><asp:Literal ID="lblFundingReached" runat="server"></asp:Literal></span>
    </div>

    <div class="meter orange nostripes">
	    <asp:Literal ID="lblMeterSpan" runat="server"></asp:Literal>
    </div>

    <ul class="dnnActions">
        <li>
            <asp:HyperLink ID="lnkFund" runat="server" CssClass="dnnSecondaryAction"></asp:HyperLink>
        </li>
    </ul>

</div>