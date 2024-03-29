﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day21
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = finalInput;

            var food = GetLines(input)
                .Select(l => SplitFoodLine(l))
                .ToList();
            
            var mapping = new Dictionary<string, HashSet<string>>();

            foreach (var f in food)
            {
                foreach (var a in f.allergens)
                {
                    HashSet<string> possibleIngredients;

                    if(!mapping.TryGetValue(a, out possibleIngredients))
                    {
                        mapping[a] = new HashSet<string>(f.ingredients.Where(i => !string.IsNullOrWhiteSpace(i)));
                    }
                    else
                    {
                        mapping[a].IntersectWith(f.ingredients);
                    }
                }
            }

            var undeterminedIngredients = mapping.Values.Where(v => v.Count > 1);
            while (undeterminedIngredients.Any())
            {
                var determined = mapping
                    .Where(kvp => kvp.Value.Count == 1)
                    .Select(kvp => kvp.Value.First())
                    .ToList();
                
                foreach (var possibleIngredients in undeterminedIngredients)
                {
                    possibleIngredients.ExceptWith(determined);
                }
            }

            var ingredientAllergenMap = mapping
                .Where(kvp => kvp.Value.Count == 1)
                .ToDictionary(kvp => kvp.Value.First(), kvp => kvp.Key);
            
            var allergenFreeIngredients = food
                .SelectMany(f => f.ingredients.Where(i => !string.IsNullOrWhiteSpace(i)))
                .Where(i => !ingredientAllergenMap.ContainsKey(i));
            
            var dangerousIngredients = mapping
                .OrderBy(kvp => kvp.Key)
                .Select(kvp => kvp.Value.First());

            Console.WriteLine(string.Join("\n", allergenFreeIngredients));
            Console.WriteLine(string.Join(",", dangerousIngredients));
            Console.WriteLine(allergenFreeIngredients.Count());
        }

        private static (string[] ingredients, string[] allergens) SplitFoodLine(string line)
        {
            int index = line.IndexOf('(');
            
            return (line.Substring(0, index).Split(' ')
            , line.Substring(index + 10, line.Length - index - 11).Split(',').Select(a => a.Trim()).ToArray());
        }

        private static IEnumerable<string> GetLines(string input)
	    {
            var inputReader = new StringReader(input);
            string currentLine = null;
                    
            while((currentLine = inputReader.ReadLine()) != null)
                yield return currentLine;
	    }
	
	    private static string testInput = @"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)";

	    private static string finalInput = @"gcrf qzgv gxgb bdvng zthrmp hjgg bjq vnd ckbpvp bfjt kqms mdjcnx qffd tqvbb tsvcsf rjgdj dtvhzt rtxjtxh djppxg hzxtr kvg ggbll cgx zxpmt jznhvh sbzd dnkgh frdr cqmxn rsjgs gdnhr ppln jncnpj glt pklfnj lvlk tlgjzx jvl mdj gqxbt klplr lsq hvgdtb kzqsv (contains wheat, eggs)
pmgnkh hjgg zthrmp pqrd dmsv rmn cqmxn dxhvs xvsbsq hsx sjqvs szkmb cnp ggjdf zps jvjpp gxgb tjc brtnbh jhtlj xxndbr cbvjd fpbkmm tlgjzx qrvxggp qrsf vdrql fpfh dggqz frdr gfljch jzjxkfh csxt hfp hgvmm sbzd ssmldnrk jznhvh tdxjk jsbpm zhpk klplr ndlkb nx dtvhzt vmxnh jcm cz ctmbr pklfnj tljbbhm hmpp snsfns pznq bjq (contains nuts, shellfish)
brfpr hjgg pmgnkh bjq gsmrzjs gfqzsbf ldsvjd xvsbsq bsdbtfc sqggjn pjl kvg qdctvn rrppgx xxndbr jznhvh dskm rhnpz jhtlj bvgvzn kqms ggbll lvlk fjgbcgd ctmbr nqvd dnkgh cnq ntl hcgf kjvvqf brcvfk ggjdf rftq fpbkmm pmmrndx xfxcj jhscqr jzjxkfh tsvcsf jsbpm hgvmm zxpmt nlflsg gfljch pznq kdqqq fxxt jrnh tlgjzx hbqprv qvdtfx pslsz vmxnh fmzm zthrmp gbnqsv klplr jcm ssmldnrk qffd vnsdg fpfh cz pklfnj zfx jvjpp vkdgddt dtvhzt kdx hph bdvng jvl (contains wheat, peanuts)
hkqtd schsv ldsvjd hpxjz jcm tlgjzx kqms ppln ghrmt jbsxv gsmrzjs dtvhzt vdrql tvxtp vkdgddt scxsjr qffd njfbrnl lxmdb dnkgh cqmxn zqmtjgsx bjq rrnn szkmb hdqrcd xvsbsq nx jznhvh tqvbb bdvng ssmldnrk rcxvm hvgdtb nlflsg ctmbr bjkpdl dxhvs fpfh zthrmp tljbbhm tqqcrs rsjgs qdctvn klplr crphf pgljb vbfsl dmsv cflkm (contains peanuts, wheat)
bjq hph xxndbr pznq fpfh qvdtfx rgnjlh ctmbr mdjcnx glt szkmb klplr pgljb sbxfq zqmtjgsx dtvhzt xvsbsq pmmrndx gbkft kqms srqkj fpbkmm sbzd lxmdb ppln jhtlj pgkkch hfp rtxjtxh ggbll hzxtr brtnbh kvqpshh hrtxrg hcgf ztjmt prlxs kdqqq gbnqsv jbsxv ndlkb vjmq tlgjzx rmn vmxnh pjl qrsf zxpmt djppxg brcvfk jvjpp cz (contains shellfish)
gxgb jvjpp czqsts hsx tqvbb fcfq pjl lxpdkv lvlk pklfnj vdrql jcm rjgdj kvqpshh zhpk hbqprv fxxt brfpr bdxczt kvg kqms ntl hrtxrg lxmdb gqxbt snsfns vbfsl ghrmt jrnh kzqsv qffd rftq qvdtfx ldsvjd hgmnj hgvmm bjq tjc dnkgh frdr zps cflkm nlflsg klplr rcxvm bjkpdl jncnpj rgnjlh dmsv hvgdtb hvcrj qrvxggp prlxs jsbpm fcccln brtnbh cz hmpp bdvng fjgbcgd mdj rtjb dskm zxpmt vnd pznq tlgjzx bskp schsv gcrf pmmrndx ggjdf bfjt mxdqd ndlkb qzgv sbzd xsrvtb dtvhzt sbxfq rrppgx tqqcrs cbvjd trkkm jznhvh xpvdq dxhvs vctm fbcz hcgf (contains sesame, wheat)
hpxjz rsvbmlrt bvgvzn brcvfk pznq ntl gqxbt jznhvh zfppfs dskm klplr ctmbr lxpdkv jrnh bvtsm zqmtjgsx mdj bjq vkdgddt nqvd nlflsg mfzr brtnbh gfqzsbf vdrql kqms crphf rjgdj jsbpm cnq rrnn lvbpsdll tlgjzx ghrmt njfbrnl vnsdg fpfh hmpp zfx pjl pmgnkh dmds snsfns xsrvtb vjmq cnp gxgb cflkm jbsxv vbxn lzplx gfljch ssmldnrk fmlxz hfp djppxg rrrns kvg qnmxmd czqsts dtvhzt gdnhr rtjb rmn fjgbcgd rsjgs rgnjlh vmxnh vfxkh fmzm kdqqq qffd lxmdb sjqvs ltxttm cqmxn mhlqnrm xfxcj (contains nuts, peanuts)
ssmldnrk jbsxv lsq cz vdrql kqms ghrmt rsjgs ctmbr kdx cbvjd jhtlj gcrf scxsjr mxdqd cqmxn tmkhj dskm gbkft lhthzgl jrnh xfxcj hmpp vnd zqmtjgsx xvsbsq fbcz lxpdkv pslsz hfp ztjmt klplr tqqcrs tljbbhm sbzd sqggjn bjq brcvfk zhpk vkdgddt tlgjzx pznq jcm gsmrzjs dxhvs dtvhzt jvjpp ckbpvp (contains eggs)
rrrns dtvhzt ntl zfppfs rtjb xvsbsq gfljch nlflsg kqms xsrvtb qffd djppxg jzjxkfh fpfh jcm rsvbmlrt tlgjzx hrtxrg zps cnq mhlqnrm mxdqd rmn hvcrj rrnn tklcd xpvdq dmds jznhvh bjq cqmxn ldsvjd fjgbcgd hgvmm hkqtd vnsdg cz glt sqggjn ghrmt nx qvdtfx jxsfn gfqzsbf tqqcrs tjc lxmdb hzxtr sbzd vkdgddt vnd tdxjk dlbx brfpr bvgvzn jbsxv jvjpp bdvng hph jhscqr kvg kdxr pznq klplr vjmq fcfq nfcvl gbkft (contains nuts)
hmpp kvqpshh fcfq fqvpbk cbvjd lvbpsdll vmxnh tqqcrs dxhvs ctmbr gdnhr bjq scxsjr jncnpj vjmq vctm dmds ggbll rrnn bsdbtfc snsfns rzmsj zps tlgjzx ggjdf pslsz sbzd pgljb hbqprv gqxbt zfx fxxt gcrf fmzm kgdqrbmq lhthzgl rrrns klplr qdctvn tljbbhm cflkm brcvfk qvdtfx nx gxgb schsv hcgf hvgdtb hgmnj tjc rjgdj jznhvh dtvhzt jkbjj (contains shellfish, soy, fish)
cgx hrtxrg nqvd kvg kqms qnmxmd frdr ckbpvp lxpdkv pjl pznq jkbjj mxdqd xxndbr ggjdf tzmn jznhvh crphf hfp tlgjzx rsvbmlrt qffd npmpqr kgdqrbmq sbxfq jxsfn kzqsv trkkm gdnhr cz brcvfk tvxtp rftq zfx bsdbtfc rtxjtxh bfjt xsrvtb gfqzsbf zthrmp jcm bvtsm lsq gcrf ghrmt dtvhzt hpxjz cflkm sbzd csxt ctmbr lzplx dmsv klplr gjcmvxb vjmq hzxtr tklcd sqggjn smp lvbpsdll hjgg kdxr zhpk rcxvm (contains soy, sesame, wheat)
nx klplr lxpdkv mdj csxt jcm ctmbr bjq pslsz lvbpsdll tmkhj ldsvjd fpfh cgx hgvmm lzplx rzmsj fjgbcgd qdctvn rmn rjgdj tdxjk dmsv tqqcrs kvqpshh szkmb bvgvzn npmpqr mxdqd sbzd fbcz schsv rtxjtxh rcxvm pgljb bdvng hbqprv prlxs tlgjzx fqvpbk pznq lvlk xpvdq fcccln zxpmt njfbrnl hgmnj ssmldnrk qzgv jznhvh jkbjj gvssx jhtlj ztjmt bskp cbvjd vfxkh kgdqrbmq hph brcvfk mdjcnx brfpr ppln rhnpz mhlqnrm ghrmt ntl zthrmp vdrql hmpp kvg jzjxkfh smp pxphfz nqvd djppxg fxxt pgkkch kqms pklfnj pmgnkh jvl rftq kdxr cflkm (contains soy)
qrsf brtnbh pmmrndx kdqqq rgnjlh cqmxn fqvpbk xpvdq ckbpvp bjq pslsz vfxkh rtxjtxh jcm tsvcsf vkdgddt hjgg kdx dnkgh ntl gbnqsv njfbrnl nx ztjmt trkkm hzxtr sbzd cz pjl zthrmp tzmn gxgb mdj xvsbsq vmxnh bvtsm sjqvs gfqzsbf xxndbr rcxvm rsjgs fcfq cflkm zhpk jbsxv dtvhzt ctmbr tlgjzx srqkj jznhvh vnd hcgf scxsjr xfxcj czqsts rtjb jhtlj tvxtp qzgv nlflsg ltxttm csxt rhnpz fpbkmm qffd snsfns klplr bjkpdl tqvbb tmkhj bdvng sbxfq hkqtd bsdbtfc mxdqd smp pznq fcccln lvbpsdll blfbr gbkft ghrmt hpxjz qnmxmd fbcz fxxt pgljb gjcmvxb tdxjk (contains shellfish, fish, eggs)
scxsjr xfxcj rmn trkkm lsq gfqzsbf bdvng fcccln dskm rsvbmlrt zthrmp qdctvn gxgb kjvvqf ctmbr tqvbb jznhvh djppxg dtvhzt pqrd jhscqr xpvdq xxndbr hjgg kqms srqkj sbzd tklcd bskp qnmxmd gsmrzjs lxpdkv bvtsm hsx rtxjtxh kvqpshh gjcmvxb hmpp brfpr fcfq pgkkch gvssx nlflsg qzgv rzmsj tjc hvgdtb jxsfn ntl dlbx ghrmt vfxkh fxxt mhlqnrm zxpmt rftq pmgnkh lhthzgl kdx ckbpvp hfp sbxfq tlgjzx zhpk bjq vnsdg lxmdb jncnpj nfcvl hph hkqtd zfx qffd bsdbtfc czqsts dnkgh ltxttm (contains shellfish, wheat, eggs)
tzmn kdqqq jhtlj gvssx nx czqsts pmmrndx jznhvh szkmb qdctvn hdqrcd rrppgx kgdqrbmq vnsdg fqvpbk bdxczt sbzd smp frdr ssmldnrk rmn pslsz dskm ppln bjq pgkkch kqms klplr dtvhzt npmpqr prlxs rtjb fpbkmm rgnjlh tdxjk tvxtp xpvdq ghrmt djppxg njfbrnl vfxkh tqqcrs blfbr rzmsj jzjxkfh brtnbh kdxr gcrf sjqvs lvbpsdll brfpr hvcrj cbvjd rjgdj jvl gxgb hjgg ctmbr hgmnj sqggjn pmgnkh rtxjtxh fcfq kdx fmzm sbxfq zhpk csxt dmsv cqmxn bvtsm pqrd fmlxz xxndbr (contains fish)
pklfnj gfljch rmn cnp zxpmt hmpp dlbx qzgv qdctvn nlflsg pmgnkh jbsxv dtvhzt pslsz smp prlxs tjc rgnjlh gjcmvxb fmzm hzxtr lxpdkv ggjdf bdxczt kjvvqf rhnpz scxsjr jxsfn tklcd tdxjk bjq mxdqd kvg gbkft ctmbr hfp ssmldnrk brtnbh njfbrnl tlgjzx crphf rsvbmlrt fxxt brfpr tljbbhm gbnqsv tzmn trkkm klplr hcgf ckbpvp fcfq tqvbb jznhvh zfppfs dmds dskm bdvng rsjgs pznq zhpk rcxvm srqkj kqms hjgg gxgb kdqqq (contains peanuts, soy, wheat)
lsq hzxtr rrppgx fcccln dskm tqqcrs ctmbr pgkkch kqms vjmq cnq njfbrnl rftq djppxg jbsxv hbqprv vdrql gxgb bjq npmpqr szkmb tsvcsf csxt sbzd kzqsv brfpr glt bjkpdl hrtxrg klplr rgnjlh ltxttm dtvhzt vfxkh fpfh brtnbh cqmxn hcgf vnsdg tlgjzx kvg czqsts xpvdq frdr lvlk hvgdtb ckbpvp pklfnj hgmnj ghrmt (contains sesame, shellfish, peanuts)
dmds dtvhzt npmpqr kjvvqf hph ctmbr tjc fqvpbk qrvxggp cgx lxmdb ndlkb xxndbr klplr fjgbcgd zfx tdxjk czqsts brcvfk sbzd kdqqq ldsvjd gbkft tvxtp rcxvm vmxnh rftq sbxfq zxpmt hkqtd ggbll rrppgx vfxkh dlbx frdr tqvbb tqqcrs csxt hgvmm kqms cz tzmn tlgjzx jznhvh gfljch tmkhj mdj jrnh dxhvs vbxn hcgf rsvbmlrt lsq brfpr pgkkch ghrmt vjmq bfjt jsbpm mdjcnx scxsjr ztjmt vctm fmlxz lxpdkv kgdqrbmq pmmrndx bsdbtfc vnd (contains peanuts)
dtvhzt jvjpp vdrql qzgv xpvdq brtnbh ctmbr gjcmvxb prlxs gbkft brfpr njfbrnl zps ndlkb cgx kzqsv hvgdtb bskp tmkhj smp xsrvtb csxt nfcvl bjq pslsz lvlk zxpmt jrnh rrrns zfppfs mfzr glt sqggjn hgvmm hgmnj klplr fmzm crphf scxsjr qdctvn tljbbhm ldsvjd kqms jcm tlgjzx czqsts sbzd pjl lvbpsdll pgljb jzjxkfh hfp vbxn kgdqrbmq sjqvs fxxt (contains wheat)
nfcvl hph dlbx xsrvtb sbxfq rsvbmlrt bskp fmlxz hgmnj djppxg sbzd fxxt xvsbsq jsbpm crphf kgdqrbmq sqggjn csxt ltxttm jbsxv hzxtr bjq rhnpz vbxn cnq tqqcrs zfx vkdgddt ctmbr fcfq gcrf jrnh cgx gqxbt dxhvs bjkpdl gbkft dggqz qvdtfx pmmrndx rgnjlh qzgv kdqqq bdvng dmds rsjgs qrvxggp jvl dtvhzt gbnqsv prlxs ggbll bsdbtfc jzjxkfh jznhvh tsvcsf rrrns kdxr mfzr lvbpsdll mdjcnx bdxczt rtjb cbvjd szkmb tlgjzx kjvvqf pjl rrppgx glt jcm klplr qnmxmd zps rrnn dmsv vdrql rcxvm cz jncnpj dskm fpbkmm jhtlj nqvd bvtsm zqmtjgsx (contains peanuts, soy, nuts)
brfpr rjgdj jhscqr cgx tqvbb qnmxmd lsq dmsv mdj gcrf hvcrj hkqtd trkkm zps bjq jbsxv ckbpvp zhpk fcccln rgnjlh vctm qvdtfx kqms jznhvh nfcvl blfbr jcm rrnn vbfsl fjgbcgd hfp vdrql rrrns fqvpbk lvlk klplr fpfh mdjcnx zfx sjqvs cflkm qrvxggp nqvd zthrmp mhlqnrm ssmldnrk cqmxn gsmrzjs nx pgljb pklfnj tqqcrs cnp jvl hvgdtb bvgvzn schsv qffd npmpqr gbnqsv dtvhzt tlgjzx zqmtjgsx smp ctmbr fpbkmm hdqrcd jzjxkfh fmlxz jsbpm cbvjd xvsbsq (contains eggs, shellfish)
mfzr pxphfz brfpr kvqpshh ckbpvp cnq dggqz bvtsm fqvpbk bdvng jxsfn nx hsx scxsjr jznhvh tzmn cqmxn zps ctmbr bjq blfbr kqms lzplx hgmnj fmlxz zxpmt lxmdb hmpp qffd vnd fpbkmm bjkpdl hgvmm xpvdq gjcmvxb cz brtnbh njfbrnl vbxn jhscqr jvjpp dtvhzt tlgjzx tvxtp xfxcj djppxg vkdgddt nqvd hjgg mdj gbnqsv vfxkh jsbpm mdjcnx sbzd dmsv hvgdtb (contains peanuts, sesame)
pslsz hcgf ntl zthrmp gqxbt dmds vdrql fbcz prlxs dtvhzt pmmrndx jznhvh rftq tzmn fxxt pmgnkh kdxr cgx vkdgddt hpxjz gcrf kvg pgljb lzplx rmn tdxjk kqms pxphfz rcxvm npmpqr ndlkb lvbpsdll hbqprv pznq crphf dggqz mdjcnx blfbr sbzd hzxtr ggbll zps vmxnh jvl hvcrj vctm gbnqsv jncnpj vjmq glt kvqpshh hgmnj hrtxrg dmsv ltxttm tqvbb vnd xvsbsq klplr jcm jxsfn tlgjzx bvtsm bvgvzn pjl hmpp bjq ldsvjd rzmsj pqrd lhthzgl cnq tsvcsf lsq qrvxggp (contains nuts, peanuts, shellfish)
ndlkb fcfq kvqpshh trkkm hbqprv hfp klplr rrppgx kqms sbxfq bdxczt rjgdj fjgbcgd rsvbmlrt jsbpm vfxkh kdqqq cnq qvdtfx glt jhscqr pgkkch vkdgddt pmgnkh ctmbr kdxr ldsvjd qdctvn zps xfxcj tlgjzx zfx lvlk vctm rftq lsq rmn xxndbr dtvhzt ltxttm jhtlj jznhvh jzjxkfh zhpk fpfh hjgg rtxjtxh kgdqrbmq cqmxn bjq jbsxv cz bskp gbkft (contains nuts, shellfish, wheat)
qnmxmd kqms gbnqsv vjmq jhtlj fcfq dtvhzt cgx tqqcrs bsdbtfc ppln jvl jxsfn scxsjr cz dxhvs pklfnj gvssx hph qvdtfx ltxttm jznhvh rrnn kvqpshh klplr fbcz pmgnkh bdvng ghrmt ctmbr tdxjk pxphfz bjq gcrf tlgjzx mdjcnx jhscqr tqvbb xfxcj cnp fmlxz rtjb bvtsm jrnh hvgdtb frdr fpbkmm gdnhr dggqz qrsf (contains shellfish, nuts, eggs)
tljbbhm ctmbr glt zps rftq blfbr hrtxrg hdqrcd dmds ggjdf pgljb dxhvs dskm gvssx cnq fpbkmm nqvd jhscqr kgdqrbmq bsdbtfc bfjt hjgg hmpp bdvng bvgvzn zfppfs klplr hsx ggbll hcgf pjl ndlkb hgmnj zxpmt scxsjr jznhvh dtvhzt sbzd nfcvl czqsts jvjpp lxmdb ztjmt qrsf bjq tmkhj tlgjzx qvdtfx zfx rrrns (contains shellfish)
hpxjz lvbpsdll lvlk npmpqr hsx scxsjr snsfns rrppgx mdjcnx qrvxggp ltxttm cnp hgvmm njfbrnl dtvhzt pgljb jrnh bfjt lzplx brtnbh sbxfq xpvdq kjvvqf jxsfn lxmdb xsrvtb dskm hcgf jznhvh jkbjj kgdqrbmq jhscqr dmsv vmxnh glt hfp pmgnkh rtjb hvcrj rftq pxphfz ctmbr rcxvm fpbkmm zqmtjgsx hkqtd fcfq rrrns brfpr sbzd tlgjzx jncnpj tvxtp kqms gbnqsv tqqcrs bjq fpfh nlflsg bvgvzn mfzr vjmq ckbpvp djppxg rmn mhlqnrm gbkft ggbll mxdqd pmmrndx vnsdg (contains soy, shellfish, peanuts)
xsrvtb fpbkmm bjq hpxjz jbsxv vkdgddt pgkkch tlgjzx kqms rrnn pjl kdqqq jrnh bskp jvjpp vmxnh rrrns vnd bsdbtfc gxgb jhtlj dtvhzt hkqtd ctmbr qrvxggp tqvbb smp zthrmp qvdtfx kgdqrbmq xpvdq ggjdf rcxvm ndlkb tzmn hjgg hgmnj jhscqr mdjcnx gfqzsbf rftq brtnbh pklfnj fqvpbk sbzd vfxkh tsvcsf klplr dmds crphf hgvmm lzplx mfzr kdxr vnsdg ppln cnq (contains sesame, soy)
nlflsg tmkhj mdj dtvhzt nx ckbpvp pznq vnsdg bdvng qnmxmd mfzr frdr rsvbmlrt klplr ctmbr npmpqr zfppfs gsmrzjs brcvfk bdxczt rhnpz fbcz sbzd bjkpdl smp pgljb csxt jbsxv kqms qdctvn tlgjzx fcfq vctm lvlk jzjxkfh tvxtp rftq hrtxrg bjq hjgg xxndbr kdx szkmb crphf ldsvjd jhscqr rtxjtxh vjmq kgdqrbmq mxdqd mdjcnx pklfnj dmsv dxhvs kvqpshh cqmxn vmxnh fmzm kvg trkkm rmn gxgb (contains nuts, shellfish, sesame)
ctmbr jvjpp qrvxggp fmzm fqvpbk zfx qnmxmd tsvcsf gfqzsbf zqmtjgsx hgmnj tlgjzx hvgdtb pjl vdrql jznhvh dskm vbfsl vkdgddt njfbrnl qvdtfx rtjb gsmrzjs sjqvs pgkkch rgnjlh bjkpdl dlbx brfpr scxsjr kqms jhscqr jhtlj pznq tzmn crphf pxphfz fmlxz srqkj zhpk bfjt sbzd jxsfn pklfnj gcrf xfxcj rtxjtxh rrrns fxxt npmpqr klplr cflkm ldsvjd pmgnkh gdnhr gfljch hrtxrg dmds lvbpsdll ggbll smp sbxfq rrnn brtnbh hsx cqmxn bjq hgvmm jncnpj lxmdb qrsf jrnh ntl hcgf djppxg mdjcnx fjgbcgd zthrmp kgdqrbmq hfp (contains shellfish, eggs)
hdqrcd kvqpshh xxndbr fcccln qrvxggp rrnn mdjcnx cqmxn hvgdtb dskm tsvcsf csxt lzplx hpxjz klplr dxhvs tqqcrs mhlqnrm rhnpz jxsfn qdctvn fmzm zhpk pklfnj cnq mdj dtvhzt pqrd vbxn cflkm kqms czqsts rjgdj ggbll qnmxmd qzgv bdvng rsvbmlrt hgmnj frdr xpvdq bjq jznhvh gbnqsv vnd ndlkb rzmsj brtnbh lhthzgl ltxttm lsq ssmldnrk vfxkh fpfh ckbpvp gsmrzjs kjvvqf jsbpm jncnpj bsdbtfc hvcrj tzmn blfbr hfp dmds rrppgx ctmbr sbzd bjkpdl szkmb bskp ztjmt cgx sjqvs vmxnh nqvd jkbjj (contains sesame, wheat, eggs)
hzxtr nlflsg pjl tzmn nx dxhvs qrsf tjc vdrql gjcmvxb vnsdg jxsfn fxxt xsrvtb qzgv jhscqr ctmbr jznhvh ghrmt xfxcj qffd ggjdf tmkhj dlbx mdj nfcvl hsx ntl nqvd pgkkch ndlkb tdxjk cz hfp ssmldnrk gfqzsbf ldsvjd qdctvn pznq cqmxn kvg hdqrcd fqvpbk hbqprv pqrd qvdtfx lhthzgl vjmq bjq fmlxz ckbpvp zxpmt zthrmp dtvhzt gbnqsv klplr cnq jkbjj rrnn qrvxggp dmsv kqms dggqz tlgjzx (contains eggs, soy)
sbzd kdqqq jznhvh ckbpvp dtvhzt tjc dmsv crphf kqms kjvvqf fmzm tsvcsf vdrql mdjcnx prlxs pmmrndx fqvpbk zhpk zqmtjgsx ghrmt csxt rmn hvcrj tdxjk xsrvtb tlgjzx ppln gcrf mfzr klplr fbcz blfbr glt ggjdf rtxjtxh hzxtr mxdqd ldsvjd vnsdg rzmsj jzjxkfh jhtlj fxxt cflkm vctm hjgg hpxjz bjq (contains shellfish, peanuts, fish)
ggjdf jhtlj hph djppxg njfbrnl zthrmp bdxczt tklcd tlgjzx cnp kdqqq nqvd rtxjtxh gsmrzjs klplr vkdgddt rsjgs ltxttm bdvng kgdqrbmq pqrd hcgf rrrns czqsts kdxr vbfsl dskm qnmxmd pgkkch gfljch jkbjj hbqprv nlflsg jvjpp fcfq ztjmt lxmdb dtvhzt bjq jzjxkfh sjqvs jncnpj hdqrcd sbzd snsfns brfpr ghrmt bsdbtfc hmpp kvg pgljb mdj fbcz jxsfn kqms fjgbcgd lxpdkv qdctvn ndlkb dlbx hzxtr vctm qzgv hpxjz bskp bvtsm hgvmm ctmbr vmxnh dmds cbvjd smp jvl jsbpm (contains nuts, shellfish)
lzplx tlgjzx ctmbr cqmxn cflkm jsbpm dskm bjq jznhvh mfzr djppxg rtjb bvtsm qnmxmd tklcd tmkhj kdxr njfbrnl dlbx rrrns cz klplr ckbpvp vctm prlxs ghrmt ztjmt jvjpp vnd jrnh fmzm tzmn pmgnkh srqkj npmpqr brfpr ltxttm qvdtfx fcfq fbcz pgkkch rhnpz hkqtd tvxtp dnkgh jhtlj bsdbtfc kqms fpfh rtxjtxh rgnjlh rzmsj dtvhzt (contains peanuts, fish)
trkkm gbnqsv ldsvjd qdctvn sbzd tlgjzx jzjxkfh nx tqqcrs ggbll jcm jvjpp czqsts bvtsm qzgv lxpdkv bsdbtfc vbxn pxphfz cz dskm rftq pklfnj qvdtfx brtnbh rjgdj scxsjr rrrns jhscqr gvssx dtvhzt kqms ctmbr jznhvh blfbr bjq kdqqq fmlxz fxxt hgmnj nfcvl dlbx bdxczt vfxkh cnp qffd pgljb dnkgh xpvdq jbsxv frdr mdjcnx djppxg (contains eggs, peanuts, shellfish)
bsdbtfc vkdgddt sbxfq tklcd fpfh klplr mxdqd bvtsm pmgnkh tlgjzx rgnjlh cqmxn kvg dtvhzt snsfns hvgdtb ckbpvp lvlk vdrql fjgbcgd hmpp zps pklfnj bjq cflkm czqsts kdqqq vbfsl brfpr jvjpp hdqrcd kvqpshh xxndbr fqvpbk hvcrj nx hjgg tmkhj rsjgs tjc jznhvh zxpmt dskm fpbkmm sbzd rhnpz kqms (contains nuts, wheat)
kdxr qrsf rrnn hph hgvmm dlbx xfxcj vkdgddt bjq mfzr vjmq hsx pznq bvtsm tlgjzx bdvng tklcd pslsz vfxkh rzmsj frdr bdxczt hpxjz klplr jncnpj cgx fbcz lvbpsdll npmpqr ntl rjgdj jznhvh dnkgh qvdtfx nfcvl hvgdtb zfppfs rcxvm rgnjlh fcfq cnp trkkm qzgv kqms cnq lxpdkv ppln kvqpshh zhpk brtnbh zfx jvjpp pxphfz ctmbr bskp lvlk kvg sbzd jsbpm pjl glt dmds prlxs smp jzjxkfh lsq kjvvqf pqrd hgmnj ldsvjd rsjgs mdj (contains eggs)
csxt gjcmvxb jxsfn cnp tlgjzx qvdtfx rcxvm hgmnj rtxjtxh pxphfz hdqrcd rgnjlh bfjt vbxn tjc hrtxrg lsq xsrvtb cflkm pklfnj brcvfk mdj cqmxn pmgnkh dnkgh bjq hcgf tmkhj gvssx fbcz jhscqr kqms jznhvh lxmdb ghrmt glt gxgb vdrql dtvhzt vkdgddt dlbx rrppgx jncnpj dskm rrrns frdr jsbpm zxpmt klplr bvtsm tzmn brfpr fqvpbk sjqvs pjl lzplx lvlk cz blfbr sbzd jvjpp vnd hpxjz hph (contains fish, sesame, peanuts)
sbzd bdxczt fqvpbk nqvd cnp cgx schsv hdqrcd ghrmt bjq rzmsj brcvfk fbcz blfbr gjcmvxb srqkj pgkkch rftq crphf ndlkb dtvhzt ssmldnrk zxpmt mdj lxpdkv hrtxrg nlflsg gsmrzjs lhthzgl jkbjj gvssx hzxtr ntl cnq njfbrnl jcm hvcrj ztjmt zthrmp czqsts fjgbcgd qzgv tklcd dmsv kqms tlgjzx rrppgx cz vbxn hjgg rmn hvgdtb kzqsv vnd fpbkmm rsjgs cbvjd klplr pznq rhnpz mhlqnrm qrsf fmzm gbkft lzplx dnkgh xxndbr dskm ctmbr hgvmm (contains sesame)
fpbkmm bvtsm tlgjzx dtvhzt dskm rtxjtxh pznq fbcz mfzr srqkj vbxn xxndbr fpfh hvcrj sbzd rftq hph pjl bskp snsfns hpxjz dmsv tmkhj fjgbcgd rjgdj ctmbr dnkgh jhscqr tjc jsbpm hgvmm mhlqnrm hmpp bjq xvsbsq zfppfs hrtxrg hcgf tzmn jhtlj kqms dlbx xfxcj jznhvh zxpmt zfx gsmrzjs sjqvs pqrd brfpr zps kvqpshh vbfsl (contains eggs, fish)
nx lvlk tsvcsf rcxvm lhthzgl vnsdg tlgjzx xxndbr pslsz tljbbhm vbxn csxt xpvdq brfpr lsq fbcz mxdqd sbzd hvcrj hfp lxmdb jznhvh jrnh mdjcnx hsx hmpp klplr rjgdj hph hcgf nqvd tmkhj dmds fmzm ctmbr zps vjmq kqms jxsfn bskp frdr lxpdkv sjqvs fcccln dggqz kdx vctm npmpqr sqggjn zxpmt vdrql nlflsg dxhvs brcvfk zhpk qnmxmd mhlqnrm crphf bdvng gfqzsbf hbqprv cqmxn hgmnj rtxjtxh qdctvn rftq rzmsj sbxfq rtjb rmn ndlkb tvxtp cgx bvgvzn gsmrzjs dtvhzt bdxczt pgkkch gvssx cnp bvtsm qffd fpbkmm brtnbh ntl kgdqrbmq hjgg gcrf hvgdtb (contains wheat, fish, soy)";

    }
}
