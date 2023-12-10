export default {
    getHomeData: () => {
        return {
            code: 200,
            data: {
                tableData: [
                    {
                        name: "小明",
                        age: 20
                    },
                    {
                        name: "小红",
                        age: 18
                    },
                ]
            }
        }
    }
}