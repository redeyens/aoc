﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	public static void Main()
	{
		var inputReader = new StringReader(input);
		string currentLine = null;
		IEnumerable<string> currentGroup = Enumerable.Empty<string>();
		int commonAnswersCount = 0;
		
		while((currentLine = inputReader.ReadLine()) != null)
		{
			if(string.Empty.Equals(currentLine))
			{
				if(currentGroup.Any())
				{
					commonAnswersCount += CountCommon(currentGroup);
					currentGroup = Enumerable.Empty<string>();
				}
			}
			else
				currentGroup = currentGroup.Append(currentLine);
		}
		
		if(currentGroup.Any())
			commonAnswersCount += CountCommon(currentGroup);
		
		Console.WriteLine("Done - {0}", commonAnswersCount);
	}
	
	private static int CountCommon(IEnumerable<string> answers)
	{
		int[] accumAnsw = new int[26];
		int persons = 0;
		int res = 0;
		
		foreach(var answer in answers)
		{
			persons++;
			for(int i = 0; i < answer.Length; i++)
			{
				accumAnsw[answer[i]-'a'] += 1;
			}
		}
		
		for(int i = 0; i < accumAnsw.Length; i++)
		{
			if(accumAnsw[i] == persons)
				res++;
		}
		
		return res;
	}
	
	private static int CountUnique(IEnumerable<string> answers)
	{
		HashSet<char> uniqueAnswers = new HashSet<char>();
		
		foreach(var answer in answers)
			for(int i = 0; i < answer.Length; i++)
			{
				uniqueAnswers.Add(answer[i]);
			}
		
		return uniqueAnswers.Count;
	}
	
	private static string input = @"lfnghcsvpyrdjtxozimb
mdtbnorpfalcijxvhsy
elmwjkfbihydxcpqtovsrun
tlhmsdjingyxcbfrvpo

a
a
xqh

mxdeqcinvfg
vbncrgzxqefka

oejsdfwm
fojsmewd
ewxfsouimdj
eodafjwsm
edjwsmfo

d
d
j

mgxywknlt
khgwmr
wkpezgboavm
mjgkwni
mwkcg

mqzjtgskhadecwy
nhkjeqgdtfsa

rc
cru
hcr

vuaeqdcnty
nltovzycbeidu

phcqe
chep
hpce

smjuow
jwktu
ujw

ljphnwtmugi
giltnpuwm
mgtulpkiwn
uxltinpgwm
jmglintwpu

slbguajo
lqwardbuo
zlqmdobau
ucalqob
lobua

uwtcp
umtlwc

lrcpgjwqfaybxtisoznuhe
wjpnrsxiultyabgzhcqfe

clmfrxgbqikzpsoduvjy
uoxibjmkygrsdqczplfnv

oncmzsf
onzmds
snzmo

zlmegxcnavjb
bmloajdecvwzxgn
pjgzecxvsmbhnla
fngpcvhaexzlbjm

ulhkjzxadgvc
uejbgctld

xuindfqzvpg
ugdofws

qrpvexjlywfmgbintuzk
wrtnibqvuflkeypxg
kretgquypbinwxfvl

tmrdpzxvily
itorywvlxjcd
rltdvygbxic
xyliotvrdc
xrvtdilygc

uihswfvanoyzl
owgnzcshuvaqlfiy
whrtnuoyimzlsvqfac
jiekwxbznhfvuplo

jgbtesurivfxclapnqdmkzhyw
dcnjhsqgkzyitwfplvearubxm

umovhtgaekjixwfcdnq
fcoamwkvjeqxhdbgitn
datcxweqzhirmjfnkogv
vwahorqcxfilmkgtndej
myhkadptsgcxfojinqewv

oys
syo
oys
yso
oys

fatvyeruxnilcmpd
ydacelgfmrxtvin
tgdirmynaxclqwe
jstnbxmdlcezhraiy

fkbnpgyhs
ktnhbya
ybhan
wqconhbey

zicrfu
dblyczk
eshjmczvn
cdzgofpx
rawzc

hcuypzdfalvt
dfplcsyvazrqh
lfcdvzrhyabtqup
lycegwaofdkmzpnihv
ayvzdhscplf

rvowpcnbjlfhe
hsgbdivpcjlxo

wbiplofhkmgvr
wvmrohkbfeglpi
bvklotpghrfiwm
wltkighfbrmvpo
bgvkflitwhmpor

dwmorjebnchvuaz
hyikgfenl
nykhxegq

pnsactlj
knytclorg

w
wj
w

gushnlqjctfvwiyz
shtqjuvzcgniy
cgdusyjpntzivqh
ivuzjgcnqhwtsy
vcusgtqinyzhj

fjnacgiw
ngijcfw
gcwnifjo
wvhsncikgtfj

tecvxgzsjridpa
azvtdgprjesixc
mxspejvzrdcaigkt
vristcjaexpzdg
jvipeagxdtczsr

v
f
w
w

wrsxdmlay
yxalwfs
xlatysmwe
bgqyohvpzswnlxujcak
silxayw

zxelswo
slwzpxo

aizvyoqg
ealnxdyczgpsrj
hkmwbutf

cohalxbk
suiydp

qebdumfwsjagk
cxaokgmr

r
x
a

xhey
exhy
hxey

zchi
izhc

dh
dsh
hd

lfjcmoauvzxqitdspbw
febzhrmowptvsqiyaudjkgc
ijausfvcdznmpqowtb
vcsfzqbjoipuwadmt

up
rkpy
nzjdbg
twkl

tuepkzc
opzst

rgszfijlxneuvqcmktpa
azcjfnkuvlpegmrsq
fgjzkvusqnmlcarep
kvrcpumgsfqalezjn

tvqiwkmdf
qfdtvowsk
tvkifsrqdw
vgdxeubfkjhqzlpwt
dvkytawfqm

yhiogvbkxlzjwsafd
fylwkxhotbasivdjzg
cdjfgnkshmwyxvaliozb
xusvawyhzklgjoifbd
vkisfrjowaxhglydubtz

kqws
xqysrkw
lcjsqkmw
qwsk
koqsw

wc
cjw
wc
cw
wc

awrshieplc
ixelpycsrvw
qebsmzklpwiuocg

cxf
xf
najxvf
xfh

umgylpoz
fxwcyet

yt
t

gbeskji
ejbsqki

grmdbezfou
ufzbredgom

wmtvcfhk
cyleh
prseclyhz

cyrwzamnoxhue
hxacmyenru
ndyrashxgmecu
eilychmuraxnfbtv

fbgwal
ablhjg
agxlb

cvw
vcoz
nvmrc

prvbngaqh
qbhgpvarn
palgyevfrqnhb
vgqnbarhp
rbhavqpgn

fztoevmhwgnb
nbhfweomvg
zfmghoevwbn
vhmsfgwcbeonq
negfvwobhm

qbx
dev
hyzl
wr

ltnibrq
qrbnil
rlbinq
qinrbl
irnqbl

thfng
gfhkbd
hmucga
rhdg

fqwalrhvpyxieu
plehrstgivfx
xbcvtrpefldih
efpxrlhvdi

kculvzfnesawmty
vkztnuycwfamlse
ecvnzlumykwatsf
zatkulemcyfsvnw
tafczlusnywekmv

a
a
a
a

cktun
lnrdyfk

rexndcwvkibpflzjqymu
exivdprnjzyblfmkwc
czbknjlrfdivxypmwe
fjvgyixpedkmrwzncbl
hricxbjypenlzfdwvkom

vqebrauop
ebopavuc
upabhveo
vobeaup

iaxlnehuycwkrmzqsbp
bynlcreqpswzmxikhau

nb
ikqoh
ho
qjh

oaixhtmeqc
envpiuhkqdmjx
xhforyeqwim
hcaezwimxq

ctubrpd
dstub

vchgnkea
hkegyxivc
fpychegtvk

apxkiotyjzsqufvbld
daviftqlxopsbyjku
zblyqpajvkdsixuotf
lafqdsyjuckbtoixvp

pmlwfdcbik
xzjebqshotrnygu

cepzjtkamvnfuloqrig
vkoiepjfnztuglqmarc
pnjrlqgmkzotiuevfca
tluscvmjrzkgaeipnfqo

k
k
r

ae
ea
ae
ea
ae

ypvtzi
mtu
tjs
fjust
ufmtj

zilgoaqmr
aqopzmuvxlwrjg
ozrmnadbqgl

rbfm
r
rep
vfmr

ajrsgqiodfmwuezkltv
tqsfiwvgzmeurdkajlo
sqgywzoakturjlimfdev

wplbfyivhasrujdgzqcmxnk
tklsezcwrihvj

vshfrae
bhnocstidq
xlhsfmza
swlxah
mpksvhy

uelktdbswyjpaxqmnr
ruvwjsnyadhmzepb

jepzvwaqublirdftgcmx
lwncbxgqkvdjpztiuamf
vxmfwtlaibpjgzucdq
pfidxgumvtacwblqzj
dsgivqfnptwzmbaxucjl

gotqzew
jdfn
dub
v

nzj
jg
g
vp
n

clbgtr
tgrlbc
rtbclg
rtbylgac

uyit
iuqtpc
ituj
tui

jlsipo
hpijl
solxjip
izplej
xlpitsj

zd
grntma
w
iz

lkxgcpywohifnu
hyglocfnkpqwux

fjz
hj

fxpjdrglkntmzce
qznmtgfpljcrhd

w
hauesfy

goixwvdy
dxivyow
dowvyix
idxowvy
dvowxyi

bw
y
swg
uyw
aijx

xhkdzbts
pedonw
gqld

neslgchypjqavwr
aclkpygqtnwiosh

vboyhcijednm
neyvjcd
yvdjnec
ednvjcy

pezkudgxf
zpfxgkbedu
exkzgfdpu

oulheigbsfjn
ltnagbieju

vmyhslndzaroiqpgxutfk
dpilskvjutayrghzon
dctniozulkhpsvgrya
srhtzkyupavdngoli

aeybn
slcoixqtkgh
bw
rvfzp
uynjmpd

zexkwslndgyi
encftykmx
rhbenmkoyjx
xymnrke
kyxjbnumer

egdq
efkxg
lmnscieutozapj
wye

nk
ny
wn
nk

wnmubxsrvqzcyhtpjf
sjymafxtzhcnriubwv

msub
ubvyhkm
ulmocbq
blfcuepqm

gtsfubrozwdiapm
oqkuvhncxbzip

vfbklexjnhziwsayqtrpm
ntrzifljyqvesamhpk
hnvmkyprultqsifazoejc
ejcvmtnipfalyrhkzqs
qtiavmhrlneydpjgkszf

yxst
hgtwy

ymf
yv
zpdyb

amdhqbzjkxilowtuvgnrfpsy
fugmwydqvsihkntlxjeazbo
rsvxyzqjoadibglfthunmkw

qzbsk
bqzsk
zsbqhk
zkbsdq
qdbkzs

dwlij
jldiwn
jiwnl
ljwrbi
jiwl

nmbhlqkaovrgiudte
dbnotamqvikeglhzrx
ohgnmkrztblaidvqef
tbqvahorjgniewldmk
qrtvyobigemkdlhan

par
rpx

rq
r
r
r
r

nuxaoymrctdsikpeghzqwjb
lrnpzwbyiksdcjeumxga
efzlgxyvdubrsmjwpnakci

ftm
t

zjydrsx
syrxjdz
sjrxyzd

rxczvljiahfwmbok
olznqgdawfjtchibvmrx

ydlwknjzhugeamq
jzvylqmgdewhaukn
zyluqhnewjdamgk
lnuygmwdaehkqzj
jindyaeqzulwghmk

ujcbsanxhmltdvzeg
uznjaedcmlgstbhxv

ejxp
dypjn
lan
bwgvkrm

v
s
s
s
s

rdswzekavfj
jubothxgani

jazgdw
gazjdw
gajzdw
wdjzga

mnecwfslyp
yfntmsl
mcnfghls
xlinzojsvm

ocgxrsmheynbvuiw
handgupyeslixmwcbvr

yfzhmrpdkustegwoalbq
lyftdmopzrewsgbu
rwuglfsbkdtcnpqozme
etzuwbsoyprfdglm
dmwtljgrszpfeobiux

ealrftgbzy
blgearjyz
yngdbxclrzmea
jayfrezbglh
leargyszbf

clkedspwxzqhfojb
nsxhezwcjpkdfoqb
czowphbeskfjqxdl

pkzoadrt
dozrtapk
dtorzpka
atdpzrko

vrjyxhnqfg
yjhrnvqag

owgadqkfusicxrztl
qicupjdzgkolftw

o
q

csbaloz
cozblas
soczalb
lczabso
mtozcbsla

faljzbneuxympogctdi
cailgjmzqexdptbvfynu
wjpknbfhelcimtxrdyzg
gmyebdfipnxljczvt

j
j
j
j

getbmjnwazok
czenqpftw
nlxzweqtf
drwuzetnyl

ywmi
sgqdt
lnhzfxpbruovja
cqe
gyk

vp
dxesf
krp
ar
gz

xeo
q

myfw
qnelw
jrodw

ndajkhfmzxslvopb
ajsckbhdpqfnlgzmvx

imldxsptvaqyhcgbjn
ghnyicdvpqtljasxbm
lqjesvdhtbaxpmcginy
andpytcblvsixmghjq
tdvyxcnimahlsjqgbp

vsopuwdxhtabgej
gjahsudbxtwpeov
vsawxbgedzotphju
detawvgbouxhpsj

xriwbfaocqgztydvnelkspj
xkjfpcaqibswtdelyrgzovn
zpdexnfwalqhyjsokgbvcirt
zevfqwxcrjplgoskyitadnb
jcsvkgtwezobdnxyfqprial

uvigr
i
i
i

zyatdpgjrle
ezsyrdhxptav
kapdyuzreotm
qeryptdagz

z
z
z
z
z

sizxwqmegyflndk
fnweyjdbckugxtmpv

vjtkzol
hwjtzmqdykosv
jtzokev
xztkvojc

o
utxgqfenh
d
yvkj
kswvm

uefcqyzgalm
qafcezlmguy
cumafzlgeqy

lmfzyt
mltf
tlgfm

ne
eu
e
zen
e

tepckxywbnuqahilvr
cwqpuveanixogmbtlyk

mgyu
usyz
nyu
uy

hvnilwjcgbekmt
mljgckftviwhneq

rdaspohl
lfxjad
amdl
dla

ncjmux
hxzemjuoqnc

slhdpwkc
plhkwsc
wslphnkc

kx
kx

klbmqja
cbpikxjoaswyhg
lakbmuj
kbja

do
dl
qd
d

uwbktzrfax
xfvtkl

pvylfnsder
dnfprelvys
srlpdefnoyv
fvnesrpldy
rdfyneplsv

cietjkmyu
gkqjcrubel
gvjouc
ajuco
nsfjpdcwuh

zeacqhdkltxi
mtldxqcha
adqcxlhwt
whatcdxql

gsrhkdwnyt
wvngykhtd
otvwdnhgk
dkmtnhwg

gdq
asgqd
gkuqd
cgdaq

amoriyvlens
nfrdvltkyes

bgemhciuqtnpw
jqwtgbpcnhmui
zyogtixnwmhcqbp
gqtwinhbcmepu

jn
nj

gkqz
gvjyzmk
kagwzf

qimgwjex
qcxjemi
xjmqige
jqxgmei
emjxqi

bftzjip
fkjvzbcy
kzjthybdm
nogjbrezl

lukfvrc
lkrfucv
drbfuklvc
uflckrv

tekijdqozns
djsiekoqtn
noektqjdis
hndojsiqekt
usndjteqiokg

cw
c
cs
wc
cn

lxfgopvmh
ckpatzifnwxsjum
bfemqdyrpx

xosicnwrb
wsocbr
brwcasoy

esiz
ise
ies

dcungxvhipzorlbwsjqkt
pvgzlihbocskdnuwqxjr
jwqvrboixdchpsklzugn

jsizhem
zjsbhi
tjhzu

tgldmihkjb
hbekplin
borvflqchzauxi

eosqa
zpqgusowa
okqslrae
qofskay
olaqst

dbulmwerfxjoiha
jdiwhoumflbxra
rxbdihmofuwalj

gqzwfumsipboyjnl
forhywgnulpsbmiz

ql
qml
owq

dpbxiqjy
bxyqjpdi
yisjxpqdb

hxcrzpjvmnkgeuo
hryatdbx
rxlhytfwds

ljdozibqytwnfhvkxp
fqxiwhcyzbptkjlvnod
iptkxndvjzlyqwmhofb
efgvoxydhpjqiztwlbkrnus
vfzqpjblnidytkxaowh

onewvclt
vhcteos
vecnto

fpoisndugqmb
fqkgntpwjbmdus
elfqmcnbudgshzp
gnqpdmbfsvu
snrqdupbvgfmj

euvtmyohfqkscjdn
fydvukotsmpnjhqec
fhqumnvydstkoejc

zcoupardbfvns
siufbpodcnlva

dwkfe
nfdz
ibjodf
xdyhvltgpu
dzjnmrcasq

k
k
k
bk
ckj

tavfsckigh
hquzcnbdorlwm
ycgh

qplvetmdc
qdmvetcpl
pqmdcvtel
lpetvmdqc

lhcq
qcuk

whmnsqpkjov
wtjgu
tlcjdw

egicls
cioegs
cesgi

lxaifjhyrqgsem
mafexiryqdsjghl
xemjlkgfqyraish

zwrdcmavo
zvrcadfmo
madfozrcv

slfagzbjwotdni
mdspebtajxfiwknzlc
dqsitfarvljnbzhw
auilefztdjonwbs
tsiebdwjlnfaz

svgnwpr
nsaiguwl
xmplcdgsn
eotjkyshbqnzgf

bl
ly
iafxle

wnckbplriv
ixdbelhpgmqfscvwzj
lpwivbc
byptliwvuac
vbicwoklpyu

eskwamyvgxzqbtprculijf
eipbrxystfkgucjazwqmvl
wvplyjkrbcguzqsfteimxa
vhtrlousjbpmfzanqkwgcxyei

p
m

ntfspeyqrj
pqyernsftj
tnrspejfqy

gx
x
x
x

gizducfvmtohe
genmuitdvzkhxf
tbpyaruimeshzgwldqv
kidmjnuovehzgt
mxthzudigve

kudjgcbhna
lcajungkd
chdankjug
cekjgpanud

zqlucvfkmnpoighxb
ohcpgetlvzufinkqmbx
cunpqvgkziflbxhom
cgklbnvfxouqmpzih
kjimlchzgxpqvyuofbn

uwmz
nz
zbofx
zw

r
or
av
j
j

tvbqc
qedctbv
tqzvbc

t
t
ts
t
t

qhkbwa
awubqtzx
fqbyja
mbaiqhrt

wzcuyqanpdtsghr
zpocrshdwynaqtxvge

gpq
qeh
cq

jfltaouwri
ygbxsqdz
skd

jmlh
hlmj
hjlm
jmlh
umihknslj

ute
ltdzqf

ygkx
mywxcrd
apyx

xy
y
dy

mivusqxpzyljehgkotrf
ajlizqnpegckfohdvsxrwb

pl
lp
pl
lp

xmnaupwtcvozrfekbdshlq
dlafzmnhvpeskwoqucb
uzydhfovwmapneqlgkbcs
cjapswblfmnzqokeuvhid
dzlaovqnmsuwkhcyfpeb

wpb
bp
bp
bp
bp

pusm
umpshr
musp

xdz
z
f
ecjo
nkxz

tjmvdf
st
trn

hkfgarys
rghfasy

ljyixtumsfngvdwoqzpakecrbh
kfpuvicgrsnoetabmywjzqxlhd

az
z

rgkf
khrgn
nrgk
gkrfoc
werzgkpa

vkg
k
k
ku
ik

blruxyvehtsn
thdl
mtldh
lht
thil

rnzqadiplhbegj
qlezpgnabdrji
lnrzaqjpdigeb
rbjlnpiegqazd
jprzlegqdbnia

kgo
okg

rjm
kiml
cuxstqpwod
kghb

yckovthbrigazfxq
atriygxofzkcqh

vudbtxjpgaqfzhlkmncswyi
lgfjahksybqmncoeuwvixtrd

dmlyrgkwfjovqasxit
ylawsjxrftdegmkqvi
stqgmxfrjvlkhaydwie
mypcdrjnkgtsxvqawlfzi

ethrvfnyasdkb
eanwdyfcsbhr
xayerdsbhlf
ejdrshvywbaf

hqyfnrxjzsd
buly
yukpm
likmy

zlpditkvsawocunjqyhmrx
rdtjsunpqymlhxkvcwzoai
oaprkqhumtvlcwxdnzsyij
uxmdclkpjqrotahysizvnw
olwhpitmraqdnkxuvjsycz

srnhuf
hrsxfwynuo
gnzsfbutcrh
krnhusf

l
lsb
ujqtvol

cptnhxgdkivyjlsbrwmuf
nkbshfmrxgjdoclwivy
fxibalcmhydvkjnwgsr

txye
cjvqsulagn
kihdbf
ohpmzwr
rbtw

tgm
mgt

nhucrjmq

rubajlyszwn
nlbsjzwryau
zrwuynsjbla

flvdypkxabzm
lipzbtrmqof
ljmghurbifpzns

bsv
wsux
vs
s
sv

vfqsehx
xseqvhf
svxefqh

fipwjdl
jpilwfd
whflvdipjx

oclrqnubyhpmdks
wmkchdsqujbpxlnyo
onulmdsbyhkqp
mlubkpyhnqods
tfmesqblonudihypk

gcwf
a
v
q
o

xpqlzcfvtjgr
fxwprjglmasiqev
xljpgbrfqvo
fbvqrcgxpdljt

nxpktofqglvrsjbi
wvkotsgijlbnrufdxq

rpfew
iflep

tdy
teyvdjo
tdy

ix
gzkwchaqd
oysure

hxvkjmneycuspbwroqaligf
qkhpisjmxblnwezrgoauycfvt
emcqhiufkjbrlwyngapsovx
aixrbgcfynwelsoqkpmhjuv

ckxhmridjbfalqo
yzunwpsdlgev

ls
lzk
sl

kdpzmau
gzamukp
kpfua
pkau

usmywivnpgt
biqxymnosaklrehzj

orwcjpkbiexz
bpzikgwn
winpbkz

wobslxnhfyjtg
vqdecolzarnm

rgfloc
yufcor

tgwnebxyzkchqvoulmprdijasf
zmpdjlekigtswronhubqcvfa

fyrbd
yfbr
frby
fbkghytr

stexkgyiqpaodnm
opecuqrdnytkxaig
oxykdtnaegpiq
xtgaydoepqikn
oyktnixegqpbad

irgbyxhwm
iwgykrbx
yibgrwx

dxejzvrocb
tbxhmqscrid
cjrodlgbxz
zcbxwpjrd

l
lg
g
lg
nfi

ab
aw

vlbi
zegtwnc

dgbj
dgjybl
ardgupbsqjv

xoquygizahnwpkvds
duaoyzvgqxhkiwspn
gpvyukzaiqdnwoxsh
qpwnyzihovukasxgd

fcepzibvulyxhsjqdanwmtrg
bijrdfxcqhplwnymsezatuv
uxbcwnrmisvqhljpzdaetfy
umwzscitfjhxyrelpvdabnq
usahyzdmtqfibxcjvlepwnr

tpk
gzetqkypuhj
pknct
mfticpk
akiwtfsp

qedhju
lvdqtzehyj

vyfe
vey
yve
yve
yev

dseopbmfcuk
msfekpuodcb

jm
jm
mj
jm

jfgevdqxm
mqftev
fmeqv
etfmqv

grhalzpfsjweuyt
zteylfsaugpj
jtuzpeyflgsa
zlajntyufgkspe
fjyapletgzsu

ixhz
n

mqekbhiu
ikgbhmeq

xscglkt
ctglkxr
kxlctg

siygouxkzhetdrqcfp
eiftxuyhpkczrqdsgo
qgfryxepotiucskhdz
pfruziqyxgedkthcso
pezofhisdcxygurqkt

kp
kqyxp

hzbtkxlyipegra
lkrgehiyapzxtb
zhliaprgbkexty
axgyhlpekbiztr

mh
al
hien
zxkprdvo

xfjokhuyszwt
ujotxwsfkzyh
tfhwozsujykx

bqudfgvjeiy
djefqvibug
yvxuiedqjgbf
xuefjqgdvib
ibveujdqgnf

liyqzxwogcdhvjsr
nvcigxswpoedq

zjdgxfqwhcbm
qxhjgdwmbzcvf
cxgfmzjwbdqh
qjzwphxibcmfdg

tdflsxicwmpar
iawprsxtmcfdl
rxpafwlihsdtcm
amtwplscxrfdi

sgjmp
sojg
gsj
sjgu

pjtayfvqgubsdzh
rxwbhcekljnm

fhsmgpyblcitz
zmibgvhylwf
vigmfbzylh
iyfvglwhmzb

bprqyialnmkveohuxs
iykvuxbpshemloarqn
ivnoqlxrpyabheukms
xvqnmyhbkueslipora

ogdijpwtxuchrv
ovjhdwxptiugcr
jxutokrfwpighcvd

oizhtyglwbnjfuverqps
syaiuqhzeltbovrwjpgn
eohzgwjbtrqulsivnpy

uptch
igt

up
uhy
u
oua
hu

owtkc
oktwc
wkcto
wcokt

qosvjmkcixhdb
upaweznmgjt

pcnabt
bapct
dcbaqzuvyl
jcab

siw
iws
wsi
siw
zskiw

b
by

vdljnuawqg
daluqjvwgn

vqctdhulbpj
citavlqhjkdpe
glhsjpryofnzwvcqdt

xo
xo
xo
ox

l
vkjflgh
ld

oftdbe
jutioleh
ebcrtgo

dcwx
zgcxtb
dcqt
opfucrjikea

kjogaebchpmxzudy
yvxmabpdgznjoe

dtfrbceshaupizxqwl
wlefqdxbarzitscpuh
lrafisdhktxpzcbwuqe
tleibrqsazhufdpcxw

mwjahec
wcjme
kjrwcme
cemjw

jfgonrqkezumxdsbvaci
vgsindtuyzjbomrkqf
osfbklvjiumwznhdgp

nwmgfludzhoyb
jwmgonfzhdkiuab
huvgwsfrbdcenmotzqp
zdhgxofwbijmnu

wahstyxvugdmolp
tdalphmxsuoygwv
glpudovmtshawxy
hwstlopuyxmagvd
lgayovuwdnxptshjm

a
a
a
rca

szlidbpwxq
slbzpqdwx
qzwxhrpybls

ewhjlxfcmuidsgnbrkyoq
enmiavrkwzlobjsuyq
pisbwokteznlqjuymr

brnec
nwcrei
bnecor
ncerb
rnkedc

pqw
kbct
hviudfjyars
wbe
b

xtsbynudvwjkfalre
auwkntbfeyvxsrdlj
nyewflsxkjuartdbv
adexltvsbymrwnkujf

yima
miay
amiy
czimay

yagqmw
mwqgya
gqwyam

acqibtfwlnyvjumrdgspoxz
dxzbqownsgylajucitmrpfv
ztpcrulxjomvwbdgyfinas
scbwynpftarmgjduxliovz
mpgtuyzhxwcoibdrsanflvj

bdftwqgsaimhxvn
stmaqbxhgwvfdni
abmfshgnxvqitdw
thabinfgmvdqsxwy
dtwvbasxnhiqfgm

siomckdrlhnytjaqpzwu
cqxytvoanbulrekzwfpd
uxadwqkgptrzlycon

stwohnvrq
kfnpxvcy

ijlkvrdacmuhotxwbsqpegyn
gopemwhirtcbynxaskvulqj
aweqlciybkguopsjtmxrhvn
pwelrvhicybstoqkujgaxmn

ckpdbfs
kcdfqv
foecjkqsvbh
muzckif

zsblxdetgovunfiqwhjpkmracy
wvkxysnjdraclzgemouphbt

qmtx
xqt

dmieulgon
jfmyra
fsjma
xmsq
tvcm

xklcpvzje
lpjrvaez
zpjlev
fvpjlez

bodrkvzcmhyxulswnijt
ynilmcwzrvohtsubkdx

tewspzja
wjtsplzfega
atzsewjp

tu
but
atdnsu

y
u

qbtawmldjsy
mjqdsylbatw
tyalbmqjswd
ljsdtwbqyma

msgytaj
vygscje
qjciogk
nhpflxzrduw

vcmrtws
srwyc
wursckd
cpwrs
wsrc

ielm
pilfr
mhxlcaf
gvzosdytl

ij
avi
ylio
kaim
ijamw

cgxwjydavlhbpior
adckqejbhysinlo
ajwczfloydthbmvi
cubojhwagryild

jyqagilxfsk
yfasltokjgqx

impglqcwn
ngcaydziwp
ipbgnzwcu

lj
j
jfrb

xncgrjkpsmwailbdeouzqht
rayuhpxqsezkdlowicfnmjgbt
ijzwpaqtdxcuhegsmlkvborn
upndtklhgeoibmzcxrswaqj

ohsckdiej
dochisejk
dcsiojkeh

xcbnwtzqu
oeywhzn
jgfskirml

dsrvqfctojm
facqi
yflgqcw
cqnlfz
qefkcy

xvwadfktl
fltkxwa
wptxjalf
mtfaxlw
awtflx

vzxqjbrwlpysguonctmf
orynmphcqbfuzwtaxjs
mrunfcqjxoytszbpw
podxzyserwjbqutnfcm
sxqdfercbnopmyujtwz

anzbwplo
ingwuxc
dwrketnsv

goz
go
goa

msgopxyt
tsopygmx
gmxyustpko
xpmtsoyg
xptgmosy

ohj
ojf
jo

gvm
c
nrhjkwda

ryqudzpvg
vzugpqr

wya
wa
bajqno
gxa
da

eobwrtihcqfp
rhbkdep

spnlubhavkw
obiqgzyxdfercj

jtfq
q

jyx
wuz

bhqnegu
uehqngo
ehvqgu
edquhg
ucgyqdhe

cigbdtul
agicltduv
cgytuidl
utgcdilm
ulgejtcwfxdi

efk
fd

yekuojwtxp
odky
ngz
tpwyd

vo
og
o
o
ofj

wavejohibzrk
wijeafkrvzob
ierabjvkzwoq

bxtkfhcyigulzdp
ihjfyukgltb
nafkrhwibgyulqvm

wljfgnvr
aowxfliqpr

swkxmvhcriylgt
cgitkxhlvmrwy

ilnzkweohypu
axfcdrtsmjqbv

xolcuzgfvitedbkyr
gkldfetvpuxyorzi
fxituzlqvokersgjhyd
btkzyxiedrlfvgou
koryuemagtixfdlzv

hsubfgecvrjna
nctvf

pyeaxfmr
eyxprfam

zr
uez
zou
zqdxnsakmvifj
hz

jhbwgfrpvdx
mcnrpehvufgtb
pvzubqhfgar
ofipybhktlqrvgs

bsfu
hu

xk
kx
uk
k
k

qymaugnc
ajigmczroyxb

dxjisapbrcqnltwv
anzrjtiqxslbce
qnxgjalrtiscmyb

rkmxv
hmrkvx

nha
pqjeh
h
ahv
vah

ipl
svirqdjpmx
pjimx
whpekfzgtbi

qlshkevani
eshaknilvq

xpi
pxio
xpjoi
pnxi
piox

kxub
dkmxu
nka
cku
quxzmk

exthvjmbrl
ebsamihlxrvt
utfbrexmkljhvn
hbxmelrvtj
mlerbhxtv

ykzvcm
vfackzmy
zvmkyc

cdnqv
vcqdn
dcsqn
dcnzq

odv
jdokvasx
dqvo
dwplnvo
qsodv

ud
du
du

sfoihm
oisf
siohf
sifo
qdjkifalso

eabz
bpza
bz
ezb
bzuyvg

d
x
d
d
d

muf
m

ohtfclrqkabuvynewidxgjp
lchojyerxwianpbtfkdvqu

h
h
vh
h

uthpecmangsirovj
itzrvywgmaushc
cumrhqtasvgnfi
mvgtorachqiebsu

icrlaxzefw
ceibjhywlvzdrsao
xelqaiczmufwr
ialnczmewr

v
frwckiy

qne
zsgn

ysaxl
xqwlasey
xsyalb

ntevywd
hxungtl

snzgpbyhtlamxroec
wczfumyrjhidgsqbkavot

sapbxcfrthquln
kqafnsuhob

eaxhmlnuwco
qigzs
gkfjy
jrvdg

bztomjpxycqnkse
qcotlpesajmfk
oetdksqpcmj

pwtjdilcyx
liejpwtcdyx
ticlpwyjxd
iwldjcpytx

aoznvbfrxy
zbfvyxroan
fybavroznx
fbavnyzrox
xobyfvrnza

ioxynh
xinyho

io
uyiwon
oxi
ivo
cio

y
ny

l
lc
gl
cl
l

akxs
asxk
akxs

vglhiwm
hkv
rducpj

iszjbtaouvcdkgfylxhqnrm
cagdmhlziutfxqrkjynovsb
kmcdrvgxynzoafusjlebqtih

kqduelpmh
uplhedkmq
dupqelkh
eflyuhwqgdkp
hqplekdu

gwkfxo
fowxgk
foxkwg
xkfwog
fwkgxo

xpegdzcsoyjt
zpcsgetjydxo
stoxjgfzecvdpy
typecgxjzosd

digfewr
psxv

pntydfukzoecihxv
vtfzihungkcdxoey

ktaqydchpr
kcalptydhx
ydkprcaht

qep
pqew
qvpe
qepx

l
o
o
n

zubsojhtqep
bujqpsytoieh
upeqthsobyj

regblhcjn
djgecrtblnh
jhebcnlrg
brehclngj

lprbihju
jhzoplsnk

hgnlwjfqczu
cjyuntigwzqeh
qhwlgunfzcajv

adjmogquh
wpsodnth

brueif
teiuqrfb
ufrbei
fbieru

zeaufk
ukfeza

qk
cqkf
kq
kq
qk

dfw
ndwft
wfd

sdevru
fdsr
nbtmord

vlhxnguq
qvlgun

nkcoszpaivjqybwt
qjdzgrumbsiopkveaylcx
szypwickvjfoqbha

xbtzcjeimsnwf
vtkicgmnhbx
xnhabvqicmtdpr

uxt
utx
utx

yurfmzncxwbil
vyi
yovi
iya
ksyi

vbkmdhjzf
vbhkjzmf
mfolkbhjzv

bcl
blc
clb

groy
n
mq
wiebc

bpn
pnb
pbn
bpnw
nbzp

blgdwivs
gvilsmwbd
bwgdklisnv
busdviwlg
mwlibsgvudc

stafhuqw
aufhqs
hsqauf
xrkfmepbsnqac

ajlhmnzqfbiskyxvuoc
svfalqujboincmxzkhy
sxvlznqfajhyboimukc
vljuykzxnsiabqchfmo
fbszjynvolhamucikqx

tjhcwv
wc

jobkfqrchsxeilu
jmfiaowhpkulqrx

janucsmrw
lnjco
qgctnjz
vgjnfc
dcionzxj

ouhskqaiydtjz
oisamqzehwlyutx
uqytfipahsvzco
uapehtyqziso
yqatsuizefhmow

klsdvrhmfiqyptuebxwc
usdxgerciwlvnmpyfob
vbwxyedsrmlpiacfu

mzcf
mwc
chm

zuipejovcklydarwxqsft
cdevzkwmrjfpoqxtuylsia
frwldqezastuocvixpjyk
xkluzcosjtfvdeapwriqy

yk
yk
ky
kgy
ykv

vtrikmwcoju
qtphlzbjwovrg

pujnbx
nbupjx
ubxpjn

nikcbsdhge
hibkgcnle
nboehtpigck
likhecnbg

cuhjlgofs
khsocg
sbfoghzv
ydsngtwop

swfudijxtklq
xqvnajufkihwtdsl
ixjkwdsqtful

mvajfubtpgxwocs
pvsbofxmjwaguc
asbgcfmowxujpv
oseuibcgxfvmawjp
pmbcageuojfxswv

dvwqskngflbpx
ibtgczowdfljmhuaye
svlgnwfrpqbd

ftxykloisajqwzun
qytozbimwxuafeknj
lazjyixqmotnuwk
voxhyjzknuticqwa

tfszajmeokvupiy
tmqcvyjsuf
vtyufsmgnj

kwzrisqu
zuskiqr

v
h
h
h
w

g
g
g
g
mg

wmoigknfuqlerxcpd
xmcrguoeqfnpkwild
egmlufncqdvxropiwk
wagquoxrcfptekdinml
lxgfmeirdquowkcpn";
}