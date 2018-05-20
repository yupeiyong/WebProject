//����������ڵ�ũ�����Լ����ֽ��ա������� isaac �޸����������� Aug 2nd 2005
/*****************************************************************************
                                   ��������
*****************************************************************************/

var lunarInfo=new Array(
	0x04bd8,0x04ae0,0x0a570,0x054d5,0x0d260,0x0d950,0x16554,0x056a0,0x09ad0,0x055d2,
	0x04ae0,0x0a5b6,0x0a4d0,0x0d250,0x1d255,0x0b540,0x0d6a0,0x0ada2,0x095b0,0x14977,
	0x04970,0x0a4b0,0x0b4b5,0x06a50,0x06d40,0x1ab54,0x02b60,0x09570,0x052f2,0x04970,
	0x06566,0x0d4a0,0x0ea50,0x06e95,0x05ad0,0x02b60,0x186e3,0x092e0,0x1c8d7,0x0c950,
	0x0d4a0,0x1d8a6,0x0b550,0x056a0,0x1a5b4,0x025d0,0x092d0,0x0d2b2,0x0a950,0x0b557,
	0x06ca0,0x0b550,0x15355,0x04da0,0x0a5d0,0x14573,0x052b0,0x0a9a8,0x0e950,0x06aa0,
	0x0aea6,0x0ab50,0x04b60,0x0aae4,0x0a570,0x05260,0x0f263,0x0d950,0x05b57,0x056a0,
	0x096d0,0x04dd5,0x04ad0,0x0a4d0,0x0d4d4,0x0d250,0x0d558,0x0b540,0x0b5a0,0x195a6,
	0x095b0,0x049b0,0x0a974,0x0a4b0,0x0b27a,0x06a50,0x06d40,0x0af46,0x0ab60,0x09570,
	0x04af5,0x04970,0x064b0,0x074a3,0x0ea50,0x06b58,0x055c0,0x0ab60,0x096d5,0x092e0,
	0x0c960,0x0d954,0x0d4a0,0x0da50,0x07552,0x056a0,0x0abb7,0x025d0,0x092d0,0x0cab5,
	0x0a950,0x0b4a0,0x0baa4,0x0ad50,0x055d9,0x04ba0,0x0a5b0,0x15176,0x052b0,0x0a930,
	0x07954,0x06aa0,0x0ad50,0x05b52,0x04b60,0x0a6e6,0x0a4e0,0x0d260,0x0ea65,0x0d530,
	0x05aa0,0x076a3,0x096d0,0x04bd7,0x04ad0,0x0a4d0,0x1d0b6,0x0d250,0x0d520,0x0dd45,
	0x0b5a0,0x056d0,0x055b2,0x049b0,0x0a577,0x0a4b0,0x0aa50,0x1b255,0x06d20,0x0ada0,
	0x14b63
);

var solarMonth=new Array(31,28,31,30,31,30,31,31,30,31,30,31);
var Gan=new Array("��","��","��","��","��","��","��","��","��","��");
var Zhi=new Array("��","��","��","î","��","��","��","δ","��","��","��","��");
var Animals=new Array("��","ţ","��","��","��","��","��","��","��","��","��","��");
var solarTerm = new Array("С��","��","����","��ˮ","����","����","����","����","����","С��","â��","����","С��","����","����","����","��¶","���","��¶","˪��","����","Сѩ","��ѩ","����");
var sTermInfo = new Array(0,21208,42467,63836,85337,107014,128867,150921,173149,195551,218072,240693,263343,285989,308563,331033,353350,375494,397447,419210,440795,462224,483532,504758);
var nStr1 = new Array('��','һ','��','��','��','��','��','��','��','��','ʮ');
var nStr2 = new Array('��','ʮ','إ','ئ','��');
var nStr3 = new Array("��", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ", "��", "��");

//�������գ�*��ʾ�ż�
var sFtv = new Array(
	"0101 Ԫ��",
	"0202 ����ʪ����",
	"0207 ��Ԯ�Ϸ���",
	"0210 ���������",
	"0214 ���˽�",
	"0301 ���ʺ�����",
	"0303 ȫ��������",
	"0308 ���ʸ�Ů��",
	"0312 ֲ����",
	"0314 ���ʾ�����",
	"0315 ������Ȩ����",
	"0317 ���ʺ�����",
	"0321 ����ɭ����",
	"0321 ���������",
	"0322 ����ˮ��",
	"0323 ����������",	
	"0401 ���˽�",
	"0407 ����������",
	"0422 ���������",
	"0501 �����Ͷ���",
	"0504 ���������",
	"0505 ��ȱ��������",
	"0508 �����ʮ����",
	"0512 ���ʻ�ʿ��",
	"0515 ���ʼ�ͥ��",
	"0517 ���������",
	"0518 ���ʲ������",
	"0520 ѧ��Ӫ����",
	"0523 ����ţ����",
	"0531 ����������", 
	"0601 ���ʶ�ͯ��",
	"0605 ���绷����",
	"0606 ȫ��������",
	"0617 ���λ�Į����",
	"0623 ����ƥ����",
	"0625 ȫ��������",
	"0626 ���ʷ�����",
	"0701 ��������",
	"0707 ���ռ�����",
	"0711 �����˿���",
	"0730 ���޸�Ů��",
	"0801 ������",
	"0815 �ձ�Ͷ����",	
	"0908 ����ɨä��",
	"0910 �й���ʦ��",
	"0914 ��������",
	"0916 �����㱣����",
	"0918 ��һ�˼���",
	"0920 ���ʰ�����",
	"0927 ����������",
	"1001 �����",
	"1004 ���綯����",
	"1008 ȫ����Ѫѹ��",
	"1008 �����Ӿ���",
	"1009 ����������",
	"1013 ���籣����",	
	"1015 ����ä�˽�",
	"1016 ������ʳ��",
	"1017 ������ƶ��",
	"1022 ����ҽҩ��",
	"1024 ���Ϲ���",
	"1031 �����ڼ���",
	"1108 �й�������",
	"1109 ����������",
	"1110 ���������",
	"1112 ����ɽ����",
	"1114 ����������",	
	"1117 ���ʴ�ѧ����",
	"1121 ���������",
	"1201 ���簬�̲���",
	"1203 ����м�����",
	"1208 ��ͯ������",
	"1209 ����������",
	"1210 ������Ȩ��",
	"1212 �����±����",
	"1213 �Ͼ�����ɱ",
	"1221 ����������",
	"1224 ƽ��ҹ",
	"1225 ʥ����",
	"1226 ë�󶫵���",
	"1229 �����������"
);

//ĳ�µĵڼ������ڼ��� 5,6,7,8 ��ʾ������ 1,2,3,4 �����ڼ�
var wFtv = new Array(
	"0110 ������",
	"0520 ĸ�׽�",
	"0530 ȫ��������",
	"0630 ���׽�",
	"0911 �Ͷ���",
	"0932 ���ʺ�ƽ��",
	"0940 �������˽�",
	"0950 ���纣����",
	"1011 ����ס����",
	"1144 �ж���"
);

//ũ������
var lFtv = new Array(
	"0101 ����",
	"0115 Ԫ����",
	"0202 ��̧ͷ��",
	"0505 �����",
	"0707 ����ȵ�Ž�",
	"0714 ���",
	"0815 �����",
	"0909 ������",
	"1208 ���˽�",
	"0100 ��Ϧ"
);

//����ũ�������������������ũ������tmd��ģ���Ȼ������Ϊ�ֽ磬�ҿ�
//�ҵ�����ǰ�ĵ�����������һΪ�ֽ�ģ��ţ������ϲ�һ�� isaac 2006.1.21
//����ԭ���Ǹ���ģ���Ϊ���Լ�����գ�Ҳ��֪����Щ�����Ƕ����
//���ƶ��ಿ�ֲ��٣����ÿ���.....
function LunarDate(tY,tM,tD) {

  var TianGan = new Array(                          // �������
    "��", "��", "��", "��", "��", "��", "��", "��", "��", "��");
  var DiZhi = new Array(                            // ��֧����
    "��", "��", "��", "î", "��", "��", "��", "δ", "��", "��", "��", "��");
  var ShuXiang = new Array(                         // ��������
    "��", "ţ", "��", "��", "��", "��", "��", "��", "��", "��", "��", "��");
  var DayName = new Array("*",                      // ũ��������
    "��һ", "����", "����", "����", "����", "����", "����", "����", "����", "��ʮ",
    "ʮһ", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "ʮ��", "��ʮ",
    "إһ", "إ��", "إ��", "إ��", "إ��", "إ��", "إ��", "إ��", "إ��", "��ʮ");
  var MonName = new Array("*",                      // ũ���·���
    "��", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ", "��", "��");
  var MonthAdd = new Array(                         // ����ÿ��ǰ�������
    0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334);
  var NongLiData = new Array(                       // ũ������
    2635, 333387, 1701, 1748, 267701, 694, 2391, 133423, 1175, 396438, 3402, 3749,
    331177, 1453, 694, 201326, 2350, 465197, 3221, 3402, 400202, 2901, 1386, 267611,
    605, 2349, 137515, 2709, 464533, 1738, 2901, 330421, 1242, 2651, 199255, 1323,
    529706, 3733, 1706, 398762, 2741, 1206, 267438, 2647, 1318, 204070, 3477, 461653,
    1386, 2413, 330077, 1197, 2637, 268877, 3365, 531109, 2900, 2922, 398042, 2395,
    1179, 267415, 2635, 661067, 1701, 1748, 398772, 2742, 2391, 330031, 1175, 1611,
    200010, 3749, 527717, 1452, 2742, 332397, 2350, 3222, 268949, 3402, 3493, 133973,
    1386, 464219, 605, 2349, 334123, 2709, 2890, 267946, 2773, 592565, 1210, 2651,
    395863, 1323, 2707, 265877
  );

  this.CalcLunar = function() {
    var i, m, n, k, isEnd, bit, TheDate;
    var curYear = tY;
    var curMonth = tM+1;
    var curDay = tD;
	
    // ���㵽��ʼʱ��1921��2��8�յ�������1921-2-8(���³�һ)
    TheDate = (curYear - 1921) * 365 +  Math.floor((curYear - 1921) / 4)
            + MonthAdd[curMonth - 1] - 38 + curDay;
    if ((curYear%4)==0 && curMonth>2)
      TheDate ++;

    // ����ũ����ɡ���֧���¡���
    isEnd = 0;
    m = 0;
    while (true) {
      k = (NongLiData[m] < 4095 ? 11 : 12);
      n = k
      while (true) {
        if (n < 0) break;
        // ��ȡ NongliData[m] �ĵ� n ��������λ��ֵ
        bit = (NongLiData[m] >>> n) % 2;
        if (TheDate <= 29 + bit) {
          isEnd = 1;
          break;
        }
        TheDate -= 29 + bit;
        n --;
      }
      if (isEnd == 1) break;
      m ++;
    }

    curYear = 1921 + m;
    curMonth = k - n + 1;
    curDay = TheDate;
    if (k==12) {
      if (curMonth==(Math.floor(NongLiData[m]/65536)+1)) {
        curMonth = 1 - curMonth
      } else if (curMonth > (Math.floor(NongLiData[m] / 65536) + 1)) {
        curMonth --;
      }
    }

    // ����ũ����ɡ���֧������ ==> strLunar
    strLunar = TianGan[((curYear - 4) % 60) % 10]
             + DiZhi[((curYear - 4) % 60) % 12] + "�� "
             + ShuXiang[((curYear - 4) % 60) % 12] + " ";

    // ����ũ���¡��� ==> strLunarDay
    strLunarDay = (curMonth < 1 ? 
      ("��" + MonName[-1 * curMonth]) : MonName[curMonth]
    );
    strLunarDay += "��" + DayName[curDay];
	return (strLunar+" "+strLunarDay);
  };

  return(this.CalcLunar());
}

/*****************************************************************************
                                      ���ڼ���
*****************************************************************************/

//====================================== ����ũ�� y���������
function lYearDays(y) {
   var i, sum = 348;
   for(i=0x8000; i>0x8; i>>=1) sum += (lunarInfo[y-1900] & i)? 1: 0;
   return(sum+leapDays(y));
}

//====================================== ����ũ�� y�����µ�����
function leapDays(y) {
   if(leapMonth(y))  return((lunarInfo[y-1900] & 0x10000)? 30: 29);
   else return(0);
}

//====================================== ����ũ�� y�����ĸ��� 1-12 , û�򴫻� 0
function leapMonth(y) {
   return(lunarInfo[y-1900] & 0xf);
}

//====================================== ����ũ�� y��m�µ�������
function monthDays(y,m) {
   return( (lunarInfo[y-1900] & (0x10000>>m))? 30: 29 );
}

//====================================== ���ũ��, �����������, ����ũ���������
//                                       ����������� .year .month .day .isLeap
function Lunar(objDate) {

   var i, leap=0, temp=0;
   var offset   = (Date.UTC(objDate.getFullYear(),objDate.getMonth(),objDate.getDate()) - Date.UTC(1900,0,31))/86400000;

   for(i=1900; i<2050 && offset>0; i++) { temp=lYearDays(i); offset-=temp; }

   if(offset<0) { offset+=temp; i--; }

   this.year = i;

   leap = leapMonth(i); //���ĸ���
   this.isLeap = false;

   for(i=1; i<13 && offset>0; i++) {
      //����
      if(leap>0 && i==(leap+1) && this.isLeap==false)
         { --i; this.isLeap = true; temp = leapDays(this.year); }
      else
         { temp = monthDays(this.year, i); }

      //�������
      if(this.isLeap==true && i==(leap+1)) this.isLeap = false;

      offset -= temp;
   }

   if(offset==0 && leap>0 && i==leap+1)
      if(this.isLeap)
         { this.isLeap = false; }
      else
         { this.isLeap = true; --i; }

   if(offset<0){ offset += temp; --i; }

   this.month = i;
   this.day = offset + 1;
}

//==============================���ع��� y��ĳm+1�µ�����
function solarDays(y,m) {
   if(m==1)
      return(((y%4 == 0) && (y%100 != 0) || (y%400 == 0))? 29: 28);
   else
      return(solarMonth[m]);
}
//============================== ���� offset ���ظ�֧, 0=����
function cyclical(num) {
   return(Gan[num%10]+Zhi[num%12]);
}

//============================== ��������
function calElement(sYear,sMonth,sDay,week,lYear,lMonth,lDay,isLeap,cYear,cMonth,cDay) {

      this.isToday    = false;
      //����
      this.sYear      = sYear;   //��Ԫ��4λ����
      this.sMonth     = sMonth;  //��Ԫ������
      this.sDay       = sDay;    //��Ԫ������
      this.week       = week;    //����, 1������
      //ũ��
      this.lYear      = lYear;   //��Ԫ��4λ����
      this.lMonth     = lMonth;  //ũ��������
      this.lDay       = lDay;    //ũ��������
      this.isLeap     = isLeap;  //�Ƿ�Ϊũ������?
      //����
      this.cYear      = cYear;   //����, 2������
      this.cMonth     = cMonth;  //����, 2������
      this.cDay       = cDay;    //����, 2������

      this.color      = '';

      this.lunarFestival = ''; //ũ������
      this.solarFestival1 = ''; //��������1
	  this.solarFestival2 = ''; //��������2
	  this.solarFestival3 = ''; //��������3
      this.solarTerms = ''; //����
}

//===== ĳ��ĵ�n������Ϊ����(��0С������)
function sTerm(y,n) {
   var offDate = new Date( ( 31556925974.7*(y-1900) + sTermInfo[n]*60000  ) + Date.UTC(1900,0,6,2,5) );
   return(offDate.getUTCDate());
}

//============================== ����������� (y��,m+1��)
/*
����˵��: ���������µ������������
ʹ�÷�ʽ: OBJ = new calendar(��,��������);
  OBJ.length      ���ص��������
  OBJ.firstWeek   ���ص���һ������
  �� OBJ[����].�������� ����ȡ�ø���ֵ
  OBJ[����].isToday  �����Ƿ�Ϊ���� true �� false
  ���� OBJ[����] ���Բμ� calElement() �е�ע��
*/
function calendar(y,m) {

   var sDObj, lDObj, lY, lM, lD=1, lL, lX=0, tmp1, tmp2, tmp3;
   var cY, cM, cD; //����,����,����
   var lDPOS = new Array(3);
   var n = 0;
   var firstLM = 0;

   sDObj = new Date(y,m,1,0,0,0,0);    //����һ������


   this.length    = solarDays(y,m);    //������������
   this.firstWeek = sDObj.getDay();    //��������1�����ڼ�

   ////////���� 1900�괺�ֺ�Ϊ������(60����36)
   if(m<2) cY=cyclical(y-1900+36-1);
   else cY=cyclical(y-1900+36);
   var term2=sTerm(y,2); //��������

   ////////���� 1900��1��С����ǰΪ ������(60����12)
   var firstNode = sTerm(y,m*2) //���ص��¡��ڡ�Ϊ���տ�ʼ
   cM = cyclical((y-1900)*12+m+12);

   //����һ���� 1900/1/1 �������
   //1900/1/1�� 1970/1/1 ���25567��, 1900/1/1 ����Ϊ������(60����10)
   var dayCyclical = Date.UTC(y,m,1,0,0,0,0)/86400000+25567+10;

   for(var i=0;i<this.length;i++) {

      if(lD>lX) {
         sDObj = new Date(y,m,i+1);    //����һ������
         lDObj = new Lunar(sDObj);     //ũ��
         lY    = lDObj.year;           //ũ����
         lM    = lDObj.month;          //ũ����
         lD    = lDObj.day;            //ũ����
         lL    = lDObj.isLeap;         //ũ���Ƿ�����
         lX    = lL? leapDays(lY): monthDays(lY,lM); //ũ���������һ��

         if(n==0) firstLM = lM;
         lDPOS[n++] = i-lD+1;
      }

      //�������������·ֵ�����, �Դ���Ϊ��
      if(m==1 && (i+1)==term2) cY=cyclical(y-1900+36);
      //����������, �ԡ��ڡ�Ϊ��
      if((i+1)==firstNode) cM = cyclical((y-1900)*12+m+13);
      //����
      cD = cyclical(dayCyclical+i);

      this[i] = new calElement(y, m+1, i+1, nStr1[(i+this.firstWeek)%7],
                               lY, lM, lD++, lL,
                               cY ,cM, cD );
   }

   //����
   tmp1=sTerm(y,m*2)-1;
   tmp2=sTerm(y,m*2+1)-1;
   this[tmp1].solarTerms = solarTerm[m*2];
   this[tmp2].solarTerms = solarTerm[m*2+1];
   //if(m==3) this[tmp1].color = 'red'; //������ɫ

   //��������
   for(i in sFtv)
      if(sFtv[i].match(/^(\d{2})(\d{2})([\s\*])(.+)$/))
         if(Number(RegExp.$1)==(m+1)) {
            this[Number(RegExp.$2)-1].solarFestival1 += RegExp.$4;
            //if(RegExp.$3=='*') this[Number(RegExp.$2)-1].color = 'red';
         }

   //���ܽ���
   for(i in wFtv)
      if(wFtv[i].match(/^(\d{2})(\d)(\d)([\s\*])(.+)$/))
         if(Number(RegExp.$1)==(m+1)) {
            tmp1=Number(RegExp.$2);
            tmp2=Number(RegExp.$3);
            if(tmp1<5)
               this[((this.firstWeek>tmp2)?7:0) + 7*(tmp1-1) + tmp2 - this.firstWeek].solarFestival2 += RegExp.$5;
            else {
               tmp1 -= 5;
               tmp3 = (this.firstWeek+this.length-1)%7; //�������һ������?
               this[this.length - tmp3 - 7*tmp1 + tmp2 - (tmp2>tmp3?7:0) - 1 ].solarFestival2 += RegExp.$5;
            }
         }

   //ũ������
   for(i in lFtv)
      if(lFtv[i].match(/^(\d{2})(.{2})([\s\*])(.+)$/)) {
         tmp1=Number(RegExp.$1)-firstLM;
         if(tmp1==-11) tmp1=1;
         if(tmp1 >=0 && tmp1<n) {
            tmp2 = lDPOS[tmp1] + Number(RegExp.$2) -1;
            if( tmp2 >= 0 && tmp2<this.length && this[tmp2].isLeap!=true) {
               this[tmp2].lunarFestival += RegExp.$4;
               //if(RegExp.$3=='*') this[tmp2].color = 'red';
            }
         }
      }

   //�����ֻ������3��4��
   if(m==2 || m==3) {
      var estDay = new easter(y);
      if(m == estDay.m)
         this[estDay.d-1].solarFestival3 = this[estDay.d-1].solarFestival3+'�����';// Easter Sunday';
   }
}

//======================================= ���ظ���ĸ����(���ֺ��һ�������ܺ�ĵ�һ����)
function easter(y) {

   var term2=sTerm(y,5); //ȡ�ô�������
   var dayTerm2 = new Date(Date.UTC(y,2,term2,0,0,0,0)); //ȡ�ô��ֵĹ����������(����һ��������3��)
   var lDayTerm2 = new Lunar(dayTerm2); //ȡ��ȡ�ô���ũ��

   if(lDayTerm2.day<15) //ȡ���¸���Բ���������
      var lMlen= 15-lDayTerm2.day;
   else
      var lMlen= (lDayTerm2.isLeap? leapDays(y): monthDays(y,lDayTerm2.month)) - lDayTerm2.day + 15;

   //һ����� 1000*60*60*24 = 86400000 ����
   var l15 = new Date(dayTerm2.getTime() + 86400000*lMlen ); //�����һ����ԲΪ��������
   var dayEaster = new Date(l15.getTime() + 86400000*( 7-l15.getUTCDay() ) ); //����¸�����

   this.m = dayEaster.getUTCMonth();
   this.d = dayEaster.getUTCDate();

}

///////////////////////////////////////////////////////////////////////////////

var cld;

function drawCld(SY,SM,sD) {
	
	var s1,s2,s3,s4,s5,lObj='',yDisplay;
	cld = new calendar(SY,SM);
	
	//��ʾũ���ꡢ����
	yDisplay=LunarDate(SY,SM,sD);
	//yDisplay = cyclical(SY-1900+36) + '�� '+ Animals[(SY-4)%12] + ' ';
	
	sD--;
	
	if(sD>-1 && sD<cld.length) { //������
		
		s1 = cld[sD].lunarFestival;
		if(s1.length>0) //ũ������
			lObj += ' ' + s1.fontcolor('#FF3BEF');
		
		s2 = cld[sD].solarFestival1;
		if( s2.length>0 && (s1.length + s2.length) <= 8) //��������1
			lObj += ' ' + s2.fontcolor('#8FB4FF');
			
		s3 = cld[sD].solarFestival2;
		if( s3.length>0 && (s1.length + s2.length + s3.length) <= 8) //��������2
			lObj += ' ' + s3.fontcolor('#8FB4FF');
		
		s4 = cld[sD].solarFestival3;
		if( s4.length>0 && (s1.length + s2.length + s3.length + s4.length) <= 8) //��������3
			lObj += ' ' + s4.fontcolor('#8FB4FF');
	
		s5 = cld[sD].solarTerms;
		if( s5.length>0 && (s1.length + s2.length + s3.length + s4.length + s5.length) <= 8 ) //إ�Ľ���
			lObj += ' ' + s5.fontcolor('limegreen');
			
	}
	else  //������
		lObj = '';

	document.write( yDisplay + lObj );
}

var Today = new Date();
var tY = Today.getFullYear();
var tM = Today.getMonth();
var tD = Today.getDate();
drawCld(tY,tM,tD); //�ɴ����������ڣ���2007.6.24Ϊ drawCld(2007,5,24);
