using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPS168.WebSite
{
    public partial class announcement : System.Web.UI.Page
    {
        protected String _urlGettype;

        protected void Page_Load(object sender, EventArgs e)
        {
            _urlGettype = Request.QueryString["type"];

            switch (_urlGettype)
            {
                case "1": announcement_content(1); break;
                case "2": announcement_content(2); break;
                case "3": announcement_content(3); break;
                default: announcement_content(1); break;
            }
        }

        protected void announcement_content(int _type)
        {
            String _str = null;

            if (_type == 1)
            {
                ipt_menu_current.Value = "5";
                hl_announce_type.Text = "结款方式";
                hl_announce_type.NavigateUrl = "announcement.aspx?type=1";
                _str = "货款到达时间<br />每周一至周六下午16:00～下午18:00为网站结算时间,18:00后的货款隔日到账。<br />首次合作要求打款或其他原因急需打款,请联系相应的采购QQ。<br /><br /><br />支持的付款方式有以下三种：<br />中国工商银行<br />支付宝支付<br />快钱支付<br /><br />24x7小时服务监督电话:021-66820598<br />经理QQ：447385692<br />所有已经确认的订单货款最迟24小时内到账,请勿催促采购人员,谢谢谅解。<br />";
            }

            if (_type == 2)
            {
                ipt_menu_current.Value = "6";
                hl_announce_type.Text = "关于我们";
                hl_announce_type.NavigateUrl = "announcement.aspx?type=2";
                _str = "公司介绍<br /><br />        上海嘉之胜网络科技有限公司成立于2005年1月，旗下还拥有北美游戏物品零售网站。经过两年多的运营，嘉之胜网络在第三方虚拟物品交易业务上快速发展，也为方便广大合作伙伴能及时了解我们的动态收货信息，我们适时推出www.TPS168.com网站  。在此平台上，我们会提供极具竞争力的价格，提供最有效率，最优质的服务。TPS168的目标是以诚信待人的服务原则、周到的服务体系、我们的服务意识、全球化的服务网络，将会全速推动您和我们共同走向成功。<br />        在第三方供货平台的构建上，我们会提供最具竞争力的价格，最有效的信息，最优质的服务，稳定的货源，充足的库存，丰富的产品，涉及多款游戏，横跨多个市场。让我们的第三方供货平台能够成为供货商的首选平台，成为最放心，最省心的平台。结合国内外市场的实际情况，追求客户永远第一的理念，为所有客户提供最廉价的物品，最快速的发货，最优质的代练，帮助客户的虚拟角色在网络游戏中健康快速成长。<br /><br /><br />公司经营理念<br /><br />客户        客户的满意与成功是度量我们工作成绩最重要的标尺；<br />员工        是公司最重要的财富，员工素质及专业知识水平的提高就是公司财富的增长，<br />            员工的福利待遇及生活水平是公司经营业绩的具体体现；<br />产品        不断创新的产品是公司发展的轨迹<br />质量        产品及服务质量是公司发展的生命线；<br />市场        寻找、开拓最适合我们的市场并力争取得更高占有率；<br />管理        一切经营活动的基本方针，合理化，正规化。<br />";
            }

            if (_type == 3)
            {
                hl_announce_type.Text = "渠道合作";
                hl_announce_type.NavigateUrl = "announcement.aspx?type=3";
                _str = "如果想进行深度合作麻烦请联系QQ:447385692";
            }

            lb_announcement.Text = _str;

        }

    }
}