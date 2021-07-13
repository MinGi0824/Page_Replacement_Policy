using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Policy_Simulator
{
    class Core
    {
        private int cursor;
        public int p_frame_size;
        public Queue<Page> frame_window;
        public List<Page> pageHistory;

        public int hit;
        public int fault;
        public int migration;

        public Core(int get_frame_size)
        {
            this.cursor = 0;
            this.p_frame_size = get_frame_size;
            this.frame_window = new Queue<Page>();
            this.pageHistory = new List<Page>();
        }

        public Page.STATUS Operate(char data)
        {
            Page newPage;

            if (this.frame_window.Any<Page>(x => x.data == data))
            {
                newPage.pid = Page.CREATE_ID++;
                newPage.data = data;
                newPage.status = Page.STATUS.HIT;
                this.hit++;
                int i;

                for (i = 0; i < this.frame_window.Count; i++)
                {
                    if (this.frame_window.ElementAt(i).data == data) break;
                }
                newPage.loc = i + 1;

            }
            else
            {
                newPage.pid = Page.CREATE_ID++;
                newPage.data = data;

                if (frame_window.Count >= p_frame_size)
                {
                    newPage.status = Page.STATUS.MIGRATION;
                    this.frame_window.Dequeue();
                    cursor = p_frame_size;
                    this.migration++;
                    this.fault++;
                }
                else
                {
                    newPage.status = Page.STATUS.PAGEFAULT;
                    cursor++;
                    this.fault++;
                }

                newPage.loc = cursor;
                frame_window.Enqueue(newPage);
            }
            pageHistory.Add(newPage);

            return newPage.status;
        }

        public List<Page> GetPageInfo(Page.STATUS status)
        {
            List<Page> pages = new List<Page>();

            foreach (Page page in pageHistory)
            {
                if (page.status == status)
                {
                    pages.Add(page);
                }
            }

            return pages;
        }

    }

    class LRU
    {
        private int cursor;
        public int p_frame_size;
        public List<Page> frame_window;
        public List<Page> pageHistory;

        public int hit;
        public int fault;
        public int migration;

        public LRU(int get_frame_size)
        {
            this.cursor = 0;
            this.p_frame_size = get_frame_size;
            this.frame_window = new List<Page>();
            this.pageHistory = new List<Page>();
        }

        public Page.STATUS Operate(char data, ref int[] time, ref int current)     
        {
            Page newPage;

            if (this.frame_window.Any<Page>(x => x.data == data))       //Hit인 경우
            {
                newPage.pid = Page.CREATE_ID++;
                newPage.data = data;
                newPage.status = Page.STATUS.HIT;
                this.hit++;
                int i;

                for (i = 0; i < this.frame_window.Count; i++)
                {
                    if (this.frame_window.ElementAt(i).data == data) break;
                }

                for (int k = 0; k < p_frame_size; k++)      
                    time[k]++;

                time[i] = 0;                                          //대기시간 초기화

                newPage.loc = i + 1;

            }
            else
            {
                newPage.pid = Page.CREATE_ID++;
                newPage.data = data;

                if (frame_window.Count >= p_frame_size)         //누굴 쫒아낼지 정하는 경우
                {
                    newPage.status = Page.STATUS.MIGRATION;

                    cursor = p_frame_size;
                    this.migration++;
                    this.fault++;

                    int max = time[0]; 
                    int maxloc = 0;

                    for (int i = 1; i < p_frame_size; i++)          //time을 참고하여 가장 오래기다린 page 선택
                    {
                        if (max < time[i])
                        {
                            max = time[i];
                            maxloc = i;
                        }
                    }

                    this.frame_window.RemoveAt(maxloc);             //찾은 page 방출

                    for (int i = 0; i < p_frame_size; i++)
                        time[i]++;

                    time[maxloc] = 0;                           //page가 바뀌기때문에 대기시간 초기화
                    cursor = maxloc + 1;                        

                    newPage.loc = cursor;
                    frame_window.Insert(maxloc, newPage);       //새로운 page 대입

                }
                else                                            //page frame에 자리가 남아 있는 경우
                {
                    newPage.status = Page.STATUS.PAGEFAULT;
                    cursor++;
                    this.fault++;

                    for (int i = 0; i < p_frame_size; i++)
                    {
                        if (time[i] != 0)
                            time[i]++;
                    }
                    time[current] = 1;                   
                    current++;

                    newPage.loc = cursor;
                    frame_window.Add(newPage);
                }
            }
            pageHistory.Add(newPage);

            return newPage.status;
        }

        public List<Page> GetPageInfo(Page.STATUS status)
        {
            List<Page> pages = new List<Page>();

            foreach (Page page in pageHistory)
            {
                if (page.status == status)
                {
                    pages.Add(page);
                }
            }

            return pages;
        }

    }


    class LFU
    {
        private int cursor;
        public int p_frame_size;
        public List<Page> frame_window;
        public List<Page> pageHistory;

        public int hit;
        public int fault;
        public int migration;

        public LFU(int get_frame_size)
        {
            this.cursor = 0;
            this.p_frame_size = get_frame_size;
            this.frame_window = new List<Page>();
            this.pageHistory = new List<Page>();
        }

        public Page.STATUS Operate(char data, ref int[] time, ref int current,ref int[] freq)
        {
            Page newPage;

            if (this.frame_window.Any<Page>(x => x.data == data))           //Hit인 경우
            {
                newPage.pid = Page.CREATE_ID++;
                newPage.data = data;
                newPage.status = Page.STATUS.HIT;
                this.hit++;
                int i;

                for (i = 0; i < this.frame_window.Count; i++)
                {
                    if (this.frame_window.ElementAt(i).data == data) break;
                }
    
                for (int k = 0; k < p_frame_size; k++)                
                    time[k]++;

                time[i] = 0;                                        //대기시간 초기화
                freq[i]++;                                          //참조된 빈도수 증가

                newPage.loc = i + 1;

            }
            else
            {
                newPage.pid = Page.CREATE_ID++;
                newPage.data = data;

                if (frame_window.Count >= p_frame_size)          //누굴 쫒아낼지 정하는 경우
                {
                    newPage.status = Page.STATUS.MIGRATION;

                    cursor = p_frame_size;
                    this.migration++;
                    this.fault++;

                    int equalfreq = 0;

                    int min = freq[0];
                    int minloc = 0;
                    for (int i = 1; i < p_frame_size; i++)       //freq를 참고하여 참조된 빈도가 가장 적은 page 선택
                    {
                        if (min > freq[i])
                        {
                            min = freq[i];                        
                            minloc = i;
                            equalfreq = 0;
                        }
                        else if (min == freq[i])
                            equalfreq++;                        //빈도수가 같은 경우 카운트
                    }
                    if (equalfreq >0) {                         //빈도수가 같은 경우 

                        int[] tmp = new int[equalfreq + 1];
                        equalfreq = 0;

                        for(int i = 0; i < p_frame_size; i++)       //빈도수가 같은 page를 찾는다
                        {
                            if (min == freq[i])
                                tmp[equalfreq++] = i;
                        }

                        int max = time[tmp[0]];
                        int maxloc = tmp[0];

                        for (int i = 1; i < tmp.Length; i++)       //빈도수가 같은 page들 중에 가장 오래 기다린 page를 다시 찾는다
                        {
                            if (max < time[tmp[i]])
                            {
                                max = time[tmp[i]];
                                maxloc = tmp[i];
                            }
                        }
                        minloc = maxloc;
                    }
                    
                    this.frame_window.RemoveAt(minloc);              //찾은 page 방출
                    for (int i = 0; i < p_frame_size; i++)
                        time[i]++;

                    time[minloc] = 0;                               //page가 바뀌기때문에 대기시간 초기화
                    freq[minloc] = 1;                               //page가 바뀌기때문에 빈도수 초기화
                    cursor = minloc + 1;

                    newPage.loc = cursor;
                    frame_window.Insert(minloc, newPage);            //새로운 page 대입

                }
                else                                                  //page frame에 자리가 남아 있는 경우
                {
                    newPage.status = Page.STATUS.PAGEFAULT;
                    cursor++;
                    this.fault++;

                    for (int i = 0; i < p_frame_size; i++)
                    {
                        if (time[i] != 0)
                            time[i]++;
                    }
                    time[current] = 1;
                    freq[current]++;
                    current++;

                    newPage.loc = cursor;
                    frame_window.Add(newPage);
                }
            }
            pageHistory.Add(newPage);

            return newPage.status;
        }
    }


    class MFU
    {
        private int cursor;
        public int p_frame_size;
        public List<Page> frame_window;
        public List<Page> pageHistory;

        public int hit;
        public int fault;
        public int migration;

        public MFU(int get_frame_size)
        {
            this.cursor = 0;
            this.p_frame_size = get_frame_size;
            this.frame_window = new List<Page>();
            this.pageHistory = new List<Page>();
        }

        public Page.STATUS Operate(char data, ref int[] time, ref int current, ref int[] freq)
        {
            Page newPage;

            if (this.frame_window.Any<Page>(x => x.data == data))            //Hit인 경우
            {
                newPage.pid = Page.CREATE_ID++;
                newPage.data = data;
                newPage.status = Page.STATUS.HIT;
                this.hit++;
                int i;

                for (i = 0; i < this.frame_window.Count; i++)
                {
                    if (this.frame_window.ElementAt(i).data == data) break;
                }

                for (int k = 0; k < p_frame_size; k++)                      
                    time[k]++;

                time[i] = 0;                                                //대기시간 초기화
                freq[i]++;                                                 //참조된 빈도수 증가

                newPage.loc = i + 1;

            }
            else
            {
                newPage.pid = Page.CREATE_ID++;
                newPage.data = data;

                if (frame_window.Count >= p_frame_size)                    //누굴 쫒아낼지 정하는 경우
                {
                    newPage.status = Page.STATUS.MIGRATION;

                    cursor = p_frame_size;
                    this.migration++;
                    this.fault++;

                    int equalfreq = 0;

                    int max = freq[0];
                    int maxloc = 0;
                    for (int i = 1; i < p_frame_size; i++)                //freq를 참고하여 참조된 빈도가 가장 많은 page 선택
                    {
                        if (max < freq[i])
                        {
                            max = freq[i];
                            maxloc = i;
                            equalfreq = 0;
                        }                           
                        else if (max == freq[i])                        //빈도수가 같은 경우 카운트
                            equalfreq++;
                    }

                    if (equalfreq > 0)                                 //빈도수가 같은 경우
                    {

                        int[] tmp = new int[equalfreq + 1];             
                        equalfreq = 0;

                        for (int i = 0; i < p_frame_size; i++)       //빈도수가 같은 page를 찾는다
                        {
                            if (max == freq[i])
                                tmp[equalfreq++] = i;
                        }

                        int min = time[tmp[0]];
                        int minloc = tmp[0];

                        for (int i = 1; i < tmp.Length; i++)            //빈도수가 같은 page들 중에 가장 오래 기다린 page를 다시 찾는다
                        {
                            if (min < time[tmp[i]])
                            {
                                min = time[tmp[i]];
                                minloc = tmp[i];
                            }
                        }
                        maxloc = minloc;
                    }


                    this.frame_window.RemoveAt(maxloc);             //찾은 page 방출

                    for (int i = 0; i < p_frame_size; i++)
                        time[i]++;

                    time[maxloc] = 0;                               //page가 바뀌기때문에 대기시간 초기화
                    freq[maxloc] = 1;                               //page가 바뀌기때문에 빈도수 초기화
                    cursor = maxloc + 1;

                    newPage.loc = cursor;
                    frame_window.Insert(maxloc, newPage);           //새로운 page 대입

                }
                else                                                //page frame에 자리가 남아 있는 경우
                {
                    newPage.status = Page.STATUS.PAGEFAULT;
                    cursor++;
                    this.fault++;

                    for (int i = 0; i < p_frame_size; i++)
                    {
                        if (time[i] != 0)
                            time[i]++;
                    }
                    time[current] = 1;
                    freq[current]++;
                    current++;

                    newPage.loc = cursor;
                    frame_window.Add(newPage);
                }
            }
            pageHistory.Add(newPage);

            return newPage.status;
        }
    }

    class Referencebit
    {
        private int cursor;
        public int p_frame_size;
        public List<Page> frame_window;
        public List<Page> pageHistory;

        public int hit;
        public int fault;
        public int migration;

        public Referencebit(int get_frame_size)
        {
            this.cursor = 0;
            this.p_frame_size = get_frame_size;
            this.frame_window = new List<Page>();
            this.pageHistory = new List<Page>();
        }

        public Page.STATUS Operate(char data, ref int[] time, ref int current,ref bool[] refbit)
        {
            Page newPage;

            if (this.frame_window.Any<Page>(x => x.data == data))               //Hit인 경우
            {
                newPage.pid = Page.CREATE_ID++;
                newPage.data = data;
                newPage.status = Page.STATUS.HIT;
                this.hit++;
                int i;

                for (i = 0; i < this.frame_window.Count; i++)
                {
                    if (this.frame_window.ElementAt(i).data == data) break;
                }

                for (int k = 0; k < p_frame_size; k++)                       
                    time[k]++;
                    
                time[i] = 0;                                                  //대기시간 초기화
                refbit[i] = true;                                            //Reference bit 'true'로 변경

                newPage.loc = i + 1;

            }
            else
            {
                newPage.pid = Page.CREATE_ID++;
                newPage.data = data;

                if (frame_window.Count >= p_frame_size)                      //누굴 쫒아낼지 정하는 경우
                {
                    newPage.status = Page.STATUS.MIGRATION;

                    cursor = p_frame_size;
                    this.migration++;
                    this.fault++;


                    int refbitone = 0;

                    for (int i = 0; i < p_frame_size; i++)                  //refbit를 참고하여 'true'인 경우 찾는다
                    {
                        if (refbit[i])
                            refbitone++;                                    //refbit를 참고하여 'true' 인 경우 카운트
                    }
                    int max = time[0];
                    int maxloc = 0;

                    if (refbitone == p_frame_size)                          //모든 page의 refbit 'true' 인 경우
                    {
                        for (int i = 1; i < p_frame_size; i++)             //refbit 'true' 인 page 중에 가장 오래 기다린 page를 다시 찾는다
                        {
                            if (max < time[i])
                            {
                                max = time[i];
                                maxloc = i;
                            }
                        }
                    }
                    else
                    {
                        int[] tmp = new int[p_frame_size - refbitone];
                        refbitone = 0;
                        for (int i = 0; i < p_frame_size; i++)          //refbit를 참고하여 'false' 인 경우를 찾는다
                        {
                            if (!refbit[i])
                                tmp[refbitone++] = i;
                        }
                        max = time[tmp[0]];
                        maxloc = tmp[0];
                        for (int i = 1; i < tmp.Length; i++)            //refbit 'false' 인 page 중에 가장 오래 기다린 page를 다시 찾는다
                        {
                            if (max < time[tmp[i]])
                            {
                                max = time[tmp[i]];
                                maxloc = tmp[i];
                            }
                        }

                    }

                    this.frame_window.RemoveAt(maxloc);                 //찾은 page 방출

                    for (int i = 0; i < p_frame_size; i++)
                        time[i]++;

                    time[maxloc] = 0;                                   //page가 바뀌기때문에 대기시간 초기화
                    refbit[maxloc] = false;                             //page가 바뀌기때문에 Reference bit 초기화
                    cursor = maxloc + 1;

                    newPage.loc = cursor;
                    frame_window.Insert(maxloc, newPage);               //새로운 page 대입

                }
                else                                                  //page frame에 자리가 남아 있는 경우
                {
                    newPage.status = Page.STATUS.PAGEFAULT;
                    cursor++;
                    this.fault++;

                    for (int i = 0; i < p_frame_size; i++)
                    {
                        if (time[i] != 0)
                            time[i]++;
                    }
                    time[current] = 1;
                    current++;

                    newPage.loc = cursor;
                    frame_window.Add(newPage);
                }
            }
            pageHistory.Add(newPage);

            return newPage.status;
        }

        public List<Page> GetPageInfo(Page.STATUS status)
        {
            List<Page> pages = new List<Page>();

            foreach (Page page in pageHistory)
            {
                if (page.status == status)
                {
                    pages.Add(page);
                }
            }

            return pages;
        }

    }
}